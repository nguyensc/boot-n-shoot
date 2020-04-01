﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace SurfsUpServer
{
    class player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        //private float moveSpeed = 5f / constants.TICKS_PER_SEC;
        private bool[] inputs;
        public player(int ID, string Username, Vector3 spawnPosition)
        {
            id = ID;
            username = Username;
            position = spawnPosition;
            rotation = Quaternion.Identity;
            inputs = new bool[4];

        }

        public void Update()
        {
            Vector2 inputDirection = Vector2.Zero;
            /* if(inputs[0])
            {
                inputDirection.Y += 1;
            }
            if (inputs[1])
            {
                inputDirection.Y -= 1;
            }
            if (inputs[2])
            {
                inputDirection.X += 1;
            }
            if (inputs[3])
            {
                inputDirection.X -= 1;
            } */


            Move(inputDirection);
        }

        private void Move(Vector2 inputDirection)
        {
            Vector3 forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, new Vector3(0, 1, 0)));

            Vector3 moveDirection = right * inputDirection.X + forward * inputDirection.Y;
            //position += moveDirection * moveSpeed;
            //There's a lot of garbage calculation being done here
            //If this works, we'll just delete this entire method
            //and replace update by just sending position back to the player

            //The only reason we are keeping track of this is
            //so that other players that log on can see the other's postions.
            //This might not even be necessary for that, but it's so integral to the connection
            //that we might as well keep it in

            //I mean all that's really happening is that we get the position from the player, and
            //send it back. It might even cause problems with client-server conflict on where
            //the player is. We'll cross that bridge when we get there.

            serversend.PlayerPosition(this);
            serversend.PlayerRotation(this);
        }
        public void SetInput(Vector3 posRec, Quaternion rotationRec)
        {
            position = posRec;
            rotation = rotationRec;
        }
    }
}
