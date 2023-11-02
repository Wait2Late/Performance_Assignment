# Performance_Assignment
Assignment for Computer Technology Course

**Controls:
WASD keys to control the player.
Space to shoot.
ESC to quit.**

In my attempt to create the packaged build with Burst Compiler in my system and jobs, I encountered challenges. Despite this, the game is still capable of handling a large number of entities and moving around without causing any lag.

During the initial stages of the game, A, which uses Burst Compiler, uses higher memory usage compared to B, which does not utilize Burst Compiler.

The A, uses Burst Compiler while B, do not. For some reason Burst Compiles uses more memory. This happened during the beginning of the game.
![image](https://github.com/Wait2Late/Performance_Assignment/assets/14058950/e4dd3561-6276-47e2-9465-fa1d23486b75)

However, during prolonged gameplay, B, utilizing Burst Compiler, demonstrated consistent performance.
![image](https://github.com/Wait2Late/Performance_Assignment/assets/14058950/f2df7645-b264-4b15-bd32-52ae03ded76d)

This brings me a conclusion that the burst compiler did not change very much in memory during start to long period of time. This is very faschinating. I have learned that utilizing DOTS and ECS is most likely a standard in the industry. I did not know this before so I am glad that I got to embrace this knowledge. 

This observation led me to the conclusion that Burst Compiler's impact on memory usage remains relatively stable over extended periods, a phenomenon that I find quite fascinating. Through this assignment, I learned that adopting DOTS and ECS is likely an industry standard. Prior to this assignment, I was unaware of this, and I am grateful for the opportunity to acquire this knowledge.


I utilized Burst compilers to generate very highly optimized machine code to enhance performance. For this assignment, I created a spawner that generates hordes of entities with random movement. I also utilized ISystem and IJobEntity to generate initialization of entities and managed their movement. With practice and a deeper understanding of these systems, my learning process became increasingly smooth.
