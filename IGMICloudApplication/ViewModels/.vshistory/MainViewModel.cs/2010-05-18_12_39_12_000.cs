using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfModelViewDemoApplication.Models;

using WpfModelViewDemoApplication.Commands;

namespace WpfModelViewDemoApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DelegateCommand exitCommand;

        #region Constructor

        public StudentsModel Students { get; set; }
        public string StudentNameToAdd { get; set; }
        public int StudentScoreToAdd { get; set; }


        public MainViewModel()
        {
            Students = StudentsModel.Current;
        }

        #endregion

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private ICommand _AddStudent;
        public ICommand AddStudent
        {
            get
            {
                if (_AddStudent == null)
                {
                    _AddStudent = new DelegateCommand(delegate()
                    {
                        StudentNameToAdd.Trim();

                        if (StudentNameToAdd == "")
                        {
                            throw new ArgumentException("Please type in a name for the student.");
                        }

                        if (StudentNameToAdd.Length < 10)
                        {
                            throw new ArgumentException("We only take students whose name is longer than 10 characters.");
                        }
                        if ((StudentScoreToAdd < 60) || (StudentScoreToAdd > 100))
                        {
                            throw new ArgumentException("We only take students whose score is between 60 and 100. please give a valid score");
                        }

                        DateTime Now = DateTime.Now;
                        StringBuilder SB = new StringBuilder();
                        SB.Append("Student ");
                        SB.Append(StudentNameToAdd);
                        SB.Append(" is added @ ");
                        SB.Append(Now.ToString());

                        Students.AddAStudent(StudentNameToAdd, StudentScoreToAdd, Now, SB.ToString());
                    });
                }

                return _AddStudent;
            }
        }

    }
}
