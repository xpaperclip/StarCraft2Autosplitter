# StarCraft2Autosplitter

A [LiveSplit](https://livesplit.org/) component for auto-splitting the StarCraft II campaign and outputting the in-game times to a file on your desktop.

It does this by:
- querying the SC2 client API to get the current in-game time
- watching the `*Campaign.SC2Bank` file to determine mission completion

Currently pretty janky and only supports Wings of Liberty.
