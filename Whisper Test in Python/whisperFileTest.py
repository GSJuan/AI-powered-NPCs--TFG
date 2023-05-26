import argparse
import time
import whisper
import torch

# Function that receives a wav file name from the command line and prints the transcription of the audio file using the whisper model.
def main():
  parser = argparse.ArgumentParser()
  parser.add_argument("--model", default="medium", help="Model to use",
                      choices=["tiny", "base", "small", "medium", "large"])
  parser.add_argument("--non_english", action='store_true',
                      help="Don't use the english model.")
  parser.add_argument("--detect_lang", action='store_true',
                      help="Don't use the english model.")
  parser.add_argument("--file", default='test.wav',
                      help="Name of to the audio file")
  args = parser.parse_args()

  # Store received args
  model = args.model
  if args.model != "large" and not args.non_english:
    model = model + ".en"

  audio_file = args.file

  # Load / Download model
  print("Loading model...")
  st = time.time()
  audio_model = whisper.load_model(model)
  et = time.time()

  print(f"Model loaded in {et - st:.2f} seconds.")

  if args.detect_lang:
    
    print("Detecting language...")
    audio = whisper.load_audio(audio_file)
    audio = whisper.pad_or_trim(audio)
    mel = whisper.log_mel_spectrogram(audio).to(audio_model.device)

    st = time.time()
    _, probs = audio_model.detect_language(mel)
    et = time.time()

    print(f"Detected language: {max(probs, key=probs.get)} in {et - st:.2f} seconds.")

  else:
    print("Transcribing...")
    st = time.time()
    # Read the transcription.
    result = audio_model.transcribe(audio_file, fp16=torch.cuda.is_available())
    #result = quantized_model.transcribe(temp_file, fp16=torch.cuda.is_available())
    et = time.time()
    text = result['text'].strip()
    print(f"Transcription done in {et - st:.2f} seconds. \nText: {text}")

if __name__ == "__main__":
  main()
