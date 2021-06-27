# StarCraft2Autosplitter

A [LiveSplit](https://livesplit.org/) component for auto-splitting the StarCraft II campaign and outputting the in-game times to a file.

Currently at the proof of concept stage than a proper production quality splitter.

It does this by watching the `*Campaign.SC2Bank` file to determine mission completion and get the IGT as shown in the Mission Archives. Currently only has mission data for Wings of Liberty and Heart of the Swarm.


## Splitting

Splits are only done based on when the `.SC2Bank` file gets updated, so the RTA start and end times do not match with current standards.


## IGT timer

Optionally can also update the in-game time by querying the SC2 client API. This part is *extremely* janky.
