# ES
Repositorio que contiene el código desarrollado durante el TFG, así como los diagramas de clase, memoria del trabajo de fin de grado y algunos videos demostrativos de la ejecución de las demos, tanto para google CardBoard como para PC, que implementan la interfaz multimodal. La interfaz implementada permite a un usuario interactuar con un personaje virtual a través de lenguaje natural y un micrófono, recibiendo una respuesta coherente en un tiempo asequible, mejorando así los sistemas de conversación respecto de aquellos implementados dentro de experiencias virtuales comerciales. Se pretende estudiar la viabilidad de diversas alternativas a emplear en el desarrollo de los tres componentes principales de la interfaz: Traducción de voz a texto, generación de respuesta textual y traducción de texto a voz.
Finalmente, se integra la interfaz desarrollada en un proyecto real: Reconstrucción virtual de la ciudad de La Laguna en el Siglo XVI. Con esto, se estudia el rendimiento del sistema, así como la calidad de la interacción implementada.

Existen dos versiones de la demo de la interfaz multimodal:
- Una para [Google CardBoard](https://deusens.com/es/blog/google-cardboard-realidad-virtual#:~:text=Las%20Google%20Cardboard%20son%20unas,podr%C3%A1s%20disfrutar%20de%20experiencias%20apasionantes.), que hace uso de librerias nativas de android para el [text-to speech](https://developer.android.com/reference/android/speech/tts/TextToSpeech) y el [speech-to text](https://developer.android.com/reference/android/speech/SpeechRecognizer), así como de la [API web de OpenAI](https://platform.openai.com/docs/api-reference) para la generación de respuestas
- Otra para PC que pretende sustituir uno a uno esos tres componentes (TTS, STT y generación de respuestas) por modelos de redes neuronales ejecutados de forma local. Actualmente el único modelo implementado es [Whisper](https://github.com/ggerganov/whisper.cpp), de forma nativa en C#, para la transcripción de voz a texto.

Además, existen varias implementaciones adicionales que sirvieron como base hasta el desarrollo de las interfaces finales:
- Chatbot ejecutando [GPT-2](https://es.wikipedia.org/wiki/GPT-2) de forma local usando el [framework ONNX](https://onnx.ai/)
- Código para la creación de un conjunto de datos para fine tuning de un modelo de lenguaje
- Pruebas en python de uso del modelo Whisper de forma local, con propósito de "benchmarking"

## Videos de las demos de interfaz multimodal
- Demo NPCs PC Whisper Tiny [![Demo PC Whisper Tiny](http://img.youtube.com/vi/48tPgXdhNx4/0.jpg)](https://www.youtube.com/watch?v=48tPgXdhNx4 "TFG Demo NPCs PC Whisper Tiny")

- Demo NPCs PC Whisper Small [![Demo PC Whisper Small](http://img.youtube.com/vi/pG7f5fgPI8I/0.jpg)](https://www.youtube.com/watch?v=pG7f5fgPI8I "TFG Demo NPCs PC Whisper Small")

- Demo NPCs VR Google CardBoard [![Demo Google CardBoard](http://img.youtube.com/vi/ZcCkyIbs1rw/0.jpg)](https://www.youtube.com/watch?v=ZcCkyIbs1rw "TFG Demo NPCs Google CardBoard")
