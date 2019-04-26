# Welcome to Our Website!

* [Project Proposal](#Project-Proposal)
* [PRD](#PRD)
* [Blog](#Blog)

# Project Proposal

4/12/2019

## Team Members
* Ben Celsi:	    benjamin.d.celsi@gmail.com
* PJ Kumlue:	    pjkumlue@cs.washington.edu
* AJ Kruse:	      ajk16@cs.washington.edu
* Hannah Werbel:	hwerbel@uw.edu

## Summary

Our project is a game in which the player navigates a 3d space trying to tap blocks (or some other object, potentially spheres/lights/bubbles) to make them disappear.  Although this mechanic is initially simple, as the game progresses, new mechanics are revealed- for example, objects of different colors may do different things, such as requiring 2 hands to touch them, flying around in the space, resetting the level when touched, and requiring being touched for a certain amount of time.

## Project Description 

One characteristic of the game is that the puzzles should be tailored to VR, in that they utilize the fact that the player is fully mobile within a 3d space, forcing the player to think about the ways they are moving around to hit all the objects. Another characteristic of the game is that there is no explicit tutorial, players will learn the mechanics intuitively as they progress through the game through trial and error, and then haveto remember what action will cause the different objects to dissappear.

[More Detailed Description](https://docs.google.com/document/d/1Jo3nvmuVI_duQIhXEHqUwL5SbqTdinQtaDjaakaY4SA/edit?usp=sharing) Feedback is greatly appreciated! 

## Hardware Preferences

1. Oculus Quest. Since our game will invovle a lot of moving around (potentially quickly or in non-traditional ways), the quest is our first choice because it is wireless so there is no danger of the player getting wrapped up in the cable.
2. Vive (other VR headset)

# PRD
Our PRD can be found [here](https://docs.google.com/document/d/122QkSjIWo0ORuT68aMuipd6LAAxZwBrRgFNYhNKyHvQ/edit?usp=sharing)

# Blog


# Week 3

April 26, 2019

## What Everyone Did
To meet our goals for this week's milestones, we developed more concrete roles and specific tasks for each team member to implement. AJ researched existing projects, code repositories, and assets for various aspects of the project, which she began compiling
a [master asset list](https://docs.google.com/document/d/1_3IYWaWWG4hmq9tFfhHMzJMrd16TxXhU1g3Dy5EdnlQ). Ben created a midi audio file demo with Ableton software that plays various percussion/beat instruments on a loop. He also explored methods for audio synchronization and Unity integration for puzzle game mechanics.
PJ worked on building event and class systems for customizable timing and interactions to implement the first percussion-themed puzzle section of the game. Hannah researched ways to teleport and transport the player between sections and also put great effort into organizing and beautifying the team website.
As a team, we brainstormed percussion puzzle mechanics, object and class code frameworks, and overall environment layout.

## Code Update
PJ wrote up a basic interaction demo with sphere objects and an event framework coordinated by subscriber-like scripts. Hannah prototyped and tested a teleportation methods for players to shift between puzzles/environment spots. The team as a whole is actively testing various object interaction and animation effects for the percussion puzzle and basic environmental layout that we organized/iterated on via git version control. 

## Idea Update
We realized while starting to build the project base that custom midi-built rhythm files sounded good with many existing melodies/popular songs overlaid, which enables us to customize rhythms to our percussion puzzle mechanic based around hitting interactable/moving shapes. Moving forward, we plan to leverage this control for more creative puzzle movements and iterative/connected interactions that build on each other as the player progresses through the song (and puzzles). 

## Plan for Next Week
* Finalize details of the percussion puzzle mechanics to build a working demo
* Continue working on syncing audio snippets to objects/motions, get a smooth system working for a basic percussion demo
* Establish a good code framework/class hierarchy for the start of game and first puzzle
* Set up basic environment and layout with boxes to mark objects to interact with based on AJ's sketch
* Continue to research assets and existing projects for code tips and other inspiration

## Blocking Issues
Figuring out how to synchronize all objects (puzzle shapes) and events with audio and with player actions at the same time is proving to be a complex issue.
It will be crucial for us to build an organized code hierarchy for this to make seamless puzzles.


# Week 2

April 19, 2019

## What Everyone Did
This week, as a collective team we further developed our idea for creating a Psychedlelic Symphony experience in VR by creating a rough outline of gameplay, setting, possible interactions, and possible visual designs.
We gathered these ideas into a narrative and explanation for Tuesday's pitch to the class, accompanied by a colorful slide deck proposing our awesome project.
On Thursday, we all contributed to writing up our PRD, established milestones for the rest of the quarter, and set up initial individual roles within the team to address different project aspects. All of this was [listed in the PRD](https://docs.google.com/document/d/122QkSjIWo0ORuT68aMuipd6LAAxZwBrRgFNYhNKyHvQ/edit?usp=sharing), along with the specific assets we already picked out. 
AJ found a simple, yet elegant concert hall as the setting for our game, while Hannah and Ben explored additional assets and discussed game mechanics.

## Code Update
Team members continued to individually work on and looked at Unity/Steam tutorials to prepare for future project work. 
PJ set up the [basicc repo](https://github.com/UWRealityLab/vrcapstone19sp-team7/tree/master/PhantasiaConductor) for the project and began work on the first puzzle implementation.

## Idea Update
At the end of last week, we decided on creating an interactive, rich symphony experience, similar to Fantasia 2000, but with minigame puzzles to explore rhythms and melodies in the various orchestral sections of a song.
The project is currently titled "Psychedelic Symphony"

## Plan for Next Week
* Flesh out details on the mechanics, structure, and other features of the puzzles
* Select a song and start working on syncing audio snippets to objects/motions
* Begin coding the first puzzle for the rhythm section
* Set up basic environment and layout with boxes to mark objects to interact with
* Pick out initial basic assets and explore the physics effects on shapes

## Blocking Issues
Determining the right song and brainstorming puzzles to work in the context of the melody and rhythm will require much creativity.
It is currently unknown how difficult it will be to coordinate the audio, motions/interactions, and visuals at the minimal/most basic level, but it could require quite a bit of debugging and coding directed at precision of timing.

# Week 1

April 12, 2019

## What Everyone Did
This week, we engaged in lengthly brainstorming sessions to decide on a project idea that would let us explore the richness of VR capabilities in novel ways. After setting up a website and several collaborative tools, including a Slack channel and git branch for our webpage, we put together a polished project proposal. We did all of these things collectively as a team.

## Code Update
Team members individually worked on and looked at Unity/Steam tutorials to prepare for future project work. No project coding has been done as of yet.

## Idea Update
We decided on creating an interactive puzzle game that leverages the unique space of VR to engage and entertain users while also promoting critical and creative thinking.

## Plan for Next Week
* Flesh out details on the mechanics, structure, and other features of the puzzles and overall game
* Create a roadmap of milestones we want to accomplish
* Begin coding

## Blocking Issues
Working out the finer elements of what our puzzles will look like and how to implement them will be an interesting challenge this coming week. 

