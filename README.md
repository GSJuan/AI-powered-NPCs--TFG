# EN
Repository containing the code developed during the Bachelor's Thesis, as well as class diagrams, thesis documentation, and some demonstrative videos of the demos' execution, both for Google CardBoard and for PC. These demos implement a multimodal interface that allows a user to interact with a virtual character through natural language and a microphone, receiving a coherent response in a reasonable time, thus improving conversational systems compared to those implemented in commercial virtual experiences. The aim is to study the viability of various alternatives for developing the three main components of the interface: voice-to-text translation, text response generation, and text-to-voice translation.
Finally, the developed interface is integrated into a real project: Virtual Reconstruction of the city of La Laguna in the 16th Century. With this, the system's performance and the quality of the implemented interaction are studied. 


There are two versions of the multimodal interface demo:
- One for [Google CardBoard](https://deusens.com/es/blog/google-cardboard-realidad-virtual#:~:text=Las%20Google%20Cardboard%20son%20unas,podr%C3%A1s%20disfrutar%20de%20experiencias%20apasionantes.), which uses native Android libraries for [text-to-speech](https://developer.android.com/reference/android/speech/tts/TextToSpeech) and [speech-to-text](https://developer.android.com/reference/android/speech/SpeechRecognizer), as well as the [OpenAI Web API](https://platform.openai.com/docs/api-reference) for response generation
- Another for PC aiming to replace each of these three components (TTS, STT, and response generation) with neural network models executed locally. Currently, the only model implemented is [Whisper](https://github.com/ggerganov/whisper.cpp), natively in C#, for voice-to-text transcription, using the whisper c++ project as precompiled dlls in orter to integrate an existing c++ implementation into the C# Unity project.

In addition, there are several additional implementations that served as a basis and research intermediate steps until the development of the final interfaces:
- Chatbot running [GPT-2](https://es.wikipedia.org/wiki/GPT-2) locally using the [ONNX framework](https://onnx.ai/)
- Code for creating a dataset for fine-tuning a language model
- Python tests for local use of the Whisper model for benchmarking purposes

## Videos of the multimodal interface demos
- Demo NPCs PC Whisper Tiny [![Demo PC Whisper Tiny](http://img.youtube.com/vi/48tPgXdhNx4/0.jpg)](https://www.youtube.com/watch?v=48tPgXdhNx4 "TFG Demo NPCs PC Whisper Tiny")

- Demo NPCs PC Whisper Small [![Demo PC Whisper Small](http://img.youtube.com/vi/pG7f5fgPI8I/0.jpg)](https://www.youtube.com/watch?v=pG7f5fgPI8I "TFG Demo NPCs PC Whisper Small")

- Demo NPCs VR Google CardBoard [![Demo Google CardBoard](http://img.youtube.com/vi/ZcCkyIbs1rw/0.jpg)](https://www.youtube.com/watch?v=ZcCkyIbs1rw "TFG Demo NPCs Google CardBoard")


