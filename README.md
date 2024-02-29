# Nule

# Intro
Nule is a network library that provides a transport layer. IL weaving still hasn't been fully implemented as the last implementation only worked within the Unity Editor but not the Unity Player.

# Transport Layer Demo
This is a demo of what can be used with the library. Here a simple chat application is made.


https://github.com/ICL32/Nule/assets/63660650/5a701317-79d4-4b27-8ff7-04cde116651a



# IL - Weaving
IL weaving and Source Generation are the most popular ways of code gen within .NET. Source generation is usually preferred as it's safer and easier to implement, but functionality is limited as it only allows you to add code but doesn't let you modify it as IL weaving does. If you want to examine a close-up implementation of IL weaving. You can do so in this codebase.

https://github.com/ICL32/Nule/assets/63660650/f9aa9699-8af7-4752-816c-f37970ea88a9

The above is an example of what IL weaving looks like. Once the weaver is injected into the code base, the behavior is modified and changes what is being printed.

# Serialization
In Nule, we are only able to serialize unmanaged types as serializing data located in heap still hasn't been implemented. This means that any struct with unmanaged types or built-in unmanaged types can be serialized successfully in the RPC call.
  ![Serialization Test Results](https://github.com/ICL32/Nule/assets/63660650/4cb47a0e-3046-4914-b94e-418dad62c085)
