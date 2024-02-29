# Nule

# Intro
Nule was a network library I have been working on for a while, planning on continuing the work later. It's a low-level networking library that is integrated within Unity. The transport layer is implemented as part of the project. One of the biggest challenges in this project was getting IL weaving to work.

# IL - Weaving
IL weaving and Source Generation are the most popular ways of code gen within .NET. Source generation is usually preferred as it's safer and easier to implement, but functionality is limited as it only allows you to add code but doesn't let you modify it as IL weaving does. If you want to examine a close-up implementation of IL weaving. You can do so in this codebase.

https://github.com/ICL32/Nule/assets/63660650/f9aa9699-8af7-4752-816c-f37970ea88a9

The above is an example of what IL weaving looks like. Once the weaver is injected into the code base, the behavior is modified and changes what is being printed.
