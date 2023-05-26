using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using BlingFire;


namespace GPT2_Dialog_Generator
{ 
    public class NeuralNet
    {
        public const int VOCAB_SIZE = 50257;

        //Tokenizer model comes from BlingFire https://github.com/microsoft/BlingFire
        public string tokenizerModelPath = "D:/Code/TFG/Models/tokenizers/gpt2";
        //GPT2 model comes from github: https://github.com/onnx/models/tree/main/text/machine_comprehension/gpt2-bs
        public string gpt2ModelPath = "D:/Code/TFG/Models/onnx/gpt2-lm-head-bs-12-opt.onnx";
        ulong tokenizerHandle = 0;
        ulong tokenizerI2WHandle = 0;
        InferenceSession inferenceSession = null;
        int gpuDeviceId = 0;
        SessionOptions options;

        #region Model loading

        public NeuralNet()
        {
            LoadTokenizer();
            LoadModel();
        }
             
        public void LoadTokenizer()
        {
            Console.WriteLine($"Using BlingFire {BlingFireUtils.GetBlingFireTokVersion()}");
            Console.WriteLine($"Loading the tokenizer from {tokenizerModelPath}");
            tokenizerHandle = BlingFireUtils.LoadModel($"{tokenizerModelPath}.bin");
            tokenizerI2WHandle = BlingFireUtils.LoadModel($"{tokenizerModelPath}.i2w");
            BlingFireUtils.SetNoDummyPrefix(tokenizerHandle, false);
            BlingFireUtils.SetNoDummyPrefix(tokenizerI2WHandle, true);
        }

        public void LoadModel()
        {
            Console.WriteLine($"Loading the gpt2 model from {gpt2ModelPath}");
            //options = SessionOptions.MakeSessionOptionWithCudaProvider(gpuDeviceId);
            //options.EnableMemoryPattern = true;
            //options.EnableCpuMemArena = true;
            //options.LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_VERBOSE;
            inferenceSession = new InferenceSession(gpt2ModelPath);//, options);
        }
        
        public string getInference(string input)
        {
            int[] inputTokenized = Tokenize(input);
            var md1Output = GetOutput(inputTokenized);
            return Prediction2Word(md1Output);
        }

        #endregion

        #region Inference Logic
        
        public int[] Tokenize(string input_str)
        {
            byte[] inBytes = System.Text.Encoding.UTF8.GetBytes(input_str);
            
            int[] ids = new int[128];
            int outputCount = BlingFireUtils.TextToIds(tokenizerHandle, inBytes, inBytes.Length, ids, ids.Length, 0);
            ids = ids.Take(outputCount).ToArray();
            Console.Write("\nTokenized input: " + string.Join(",", ids) + "\n");
            return ids;
        }
        
        public IDisposableReadOnlyCollection<DisposableNamedOnnxValue> GetOutput(int[] input_ids)
        {
            Tensor<Int64> input_tensor = new DenseTensor<Int64>(new[] { 1, input_ids.Length});
            
            Tensor<Single> attention_mask = new DenseTensor<Single>(new[] { 1, input_ids.Length });
            
            for (int i = 0; i < input_ids.Length; i++)
            {
                input_tensor[0, i] = input_ids[i];
                attention_mask[0, i] = 1f;
            }
            
            Tensor<Int64> out_token_num = new DenseTensor<Int64>(new[] { 1 });
            out_token_num[0] = 13;

            var model_inputs = new List<NamedOnnxValue>()
            {
                NamedOnnxValue.CreateFromTensor("input_ids", input_tensor),
                NamedOnnxValue.CreateFromTensor("attention_mask", attention_mask),
                NamedOnnxValue.CreateFromTensor("out_token_num", out_token_num)
            };
            return inferenceSession.Run(model_inputs);            
        }
        
        public string Prediction2Word(IDisposableReadOnlyCollection<DisposableNamedOnnxValue> model_outputs)
        {
            var tensor = model_outputs.First().AsTensor<Int64>();
            Console.Write($"\nGot an ouput tensor with dimensions [{String.Join(",", tensor.Dimensions.ToArray())}]\n");
            string str = "";
            for(int i = 0; i < tensor.Dimensions[0]; i++)
            {
                Console.WriteLine("\nResult " + (i+1));
                int[] tokens = new int[tensor.Dimensions[1]];
                for (int j = 0; j < tensor.Dimensions[1]; j++)
                {
                    int token = (int)tensor[i, j];
                    tokens[j] = token;
                }
                string temp = BlingFireUtils.IdsToText(tokenizerI2WHandle, tokens, false);
                str += temp;
                Console.WriteLine(temp);
                str += "\n";
            }
            model_outputs.Dispose();
            return str;
        }
        public void Cleanup()
        {
            BlingFireUtils.FreeModel(tokenizerHandle);
            BlingFireUtils.FreeModel(tokenizerI2WHandle);
        }

        #endregion
    }

}
