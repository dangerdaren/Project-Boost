<p align="center">
  <img src="https://daren-stottrup.notion.site/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F54fca100-7136-452e-ba9c-76df8343a588%2FProject_Boost.png?table=block&id=62e21ad8-a1ca-46a8-9537-ee1a6129b313&spaceId=f2ac5bd7-db8b-4b29-8205-809cd644ec3b&width=2000&userId=&cache=v2">
</p>

# Project: Boost
To learn more about this game, visit [Project: Boost](https://daren-stottrup.notion.site/Project-Boost-7e6a700a4f894bdfba91a2e16e04ed64) on my [portfolio](https://daren-stottrup.notion.site/Game-Portfolio-3bc5aac8cfcb4d32af26f20301371155).
<br>
To play this game, check out the [WebGL Version](https://play.unity.com/mg/other/webgl-builds-22327).

## Coursework Project
This project was part of a course I took, and looking back, I'm grateful that we finally started implementing "private" and "public" keywords! Nevertheless, as with most of the coursework projects, there were additions I made to the game--features that made the project go from simple homework, to games that I felt comfortable sending to friends to play as evidence of my education.

## My Additions

#### Gravity & Mass
This one is a modification in the Unity editor: I wanted the rocket to feel heavy, rather than zippy, so I gave it a lot of mass. I also positioned the rotation origin in such a way as to make it feel like the thrusters were pushing the bulk of the rocket.

#### Kaboom!
Additionally, I made it so when the rocket collides with an obstacle, not only did it create a particle effect, but there would be a force applied to all of the individual elements of the rocket, so it would [explode into a bunch of pieces](https://github.com/dangerdaren/Project-Boost/blob/bfe22577f0c638b6c5101bc734315ed02e2c87b7/Assets/Scripts/Rocket.cs#L110-L128). Since I also added lights to the thrusters and a headlight at the top, those added a nice extra flair to the flying bits.

#### Gentle Ramp-Up / Infinite Retry
Finally, when I had someone else test it, they found it more difficult than I had, so I added a bunch of easier levels that slowly ramp up, before the final two levels that are more involved. And rather than simply end the game, I have it [loop back to the beginning](https://github.com/dangerdaren/Project-Boost/blob/bfe22577f0c638b6c5101bc734315ed02e2c87b7/Assets/Scripts/Rocket.cs#L136-L146), so that the player can push themselves to see how fast they can accomplish each level.
