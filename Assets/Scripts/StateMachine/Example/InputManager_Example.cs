using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assets.StateMachine.Example
{
    interface ICommand
    {
        void Execute();
    }
    class InputManager_Example
    {
        public ICommand ACommand;
        public ICommand BCommand;
        public ICommand XCommand;
        public ICommand YCommand;

        public void HandleInput(ICommand pressed)//<T>
        {
            bool keypress = true;
        //input of player
        if(keypress == true)
            {
               // ICommand pressed = new ICommand();
                pressed.Execute();
                //ACommand.Execute();
            }
       
        }
    }

    class CommandClass: ICommand //implement interface
    {
        public void Execute()
        {
            //do something
            InputManager_Example test = new InputManager_Example();
            test.HandleInput(test.XCommand); //give a command to the inputmanager.handleinput
        }
    }
}
//https://hku.instructure.com/courses/1330/files/folder/Slides?preview=6156\\
