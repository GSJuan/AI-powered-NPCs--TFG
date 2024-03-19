# English:
## Important questions for use:

Minimum Android version: Android 7.0

Tested Android version: Android 10.0

Requires use of microphone, check if the Google application has microphone permission.

## Programming milestones:

We have achieved a scene with complex components such as NPCs that also have movement animations, all using free assets.

We developed a multimodal interface to communicate with NPCs which includes the following technologies:
- Use of packages for text conversion of user input and audio generation based on NPC response using native Android libraries.
- Use of Android library for permission checking and request associated with the application.
- Use of specific compiler directives for platform distinction and subsequent resource allocation.
- Use of library relying on web API usage for processing the user's input text.
- Use of events for the execution of certain functions dependent on certain user actions (start and end of conversation).
- Use of raycast for the detection of visual contact between the user and the NPC addressed.
- Use of external tool to provide movement to the different joints of the NPCs.
- We learned how to debug native Android applications.
- Use of scriptable objects to store information.

## Highlights:

- Higher performance than expected: The speech-to-text and text-to-speech, being implemented in native Android libraries, are very fast.
- However, the part of processing the user's input text as it relies on the use of a web API introduces latency in the overall system execution.
- NPCs are described using a scriptable object that contains all the information associated with each individual NPC (Name, gender, occupation, brief description, and examples of conversation).
- The content of the NPC description and the conversation examples were generated using ChatGPT.

## Link to the video of the execution:

- Demo NPCs VR Google CardBoard [![Demo Google CardBoard](http://img.youtube.com/vi/ZcCkyIbs1rw/0.jpg)](https://www.youtube.com/watch?v=ZcCkyIbs1rw "TFG Demo NPCs Google CardBoard")

- BETA Demo [Video](https://drive.google.com/file/d/1B93Us3VGfMXEUgijWwgMgUeUyNCyubeY/view?usp=sharing)

## Link to explanatory slides

[Slides](https://docs.google.com/presentation/d/1Fxsn4aJ7tmsBcNgAlFrFgiSzafDgbOZXP2k7qyv_UNI/edit?usp=sharing)

## Execution gif:

![Gif](https://github.com/alu0101325583/Gpt_Powered_Npcs_Vr_Demo/blob/main/Screen_Recording_20230118-202102_GPT3_Npcs_AdobeExpress.gif)
