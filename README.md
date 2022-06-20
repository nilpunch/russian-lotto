# Russian Lotto
Example of Unity game with Photon multiplayer, that highly abstracted from both Unity engine and Photon network.

Checkout [`dev`](https://github.com/nilpunch/russian-lotto/tree/dev) branch for last updates.
## From proof of concept to real world example
There's no need to couple your clean client program with trash Photon netcode and move on with master-client architecture. Roll interfaces for everything and separate your client code from the master-client at the application's entry point, so that it can be easily removed later.

Most of the OOP in the project tried to follow the [opus](https://www.notion.so/forcepusher/Objects-Interfaces-State-of-the-art-9cc3571e7a3f474597963be398d03224).
