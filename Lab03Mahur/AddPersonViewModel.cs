using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab03Mahur.Annotations;
using Lab03Mahur.Exceptions;

namespace Lab03Mahur
{
    internal class AddPersonViewModel : INotifyPropertyChanged
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;
        private DateTime _dateOfBirth = DateTime.Today;
        private RelayCommand _proceed;
        private readonly Action<bool> _showLoaderAction;

        internal AddPersonViewModel(Action<bool> showLoader)
        {
            _showLoaderAction = showLoader;
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {

                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {      
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand Proceed
        {
            get
            {
                return _proceed ?? (_proceed = new RelayCommand(CreatePersonImpl, x =>
                               !String.IsNullOrWhiteSpace(_firstName) &&
                               !String.IsNullOrWhiteSpace(_lastName) &&
                               !String.IsNullOrWhiteSpace(_email) &&
                               _dateOfBirth != DateTime.MinValue));
            }
        }

        private async void CreatePersonImpl(object o)
        {
            Person newPerson = null;

            _showLoaderAction.Invoke(true);
            await Task.Run(() =>
            {
                try
                {
                    newPerson = new Person(_firstName, _lastName, _email, _dateOfBirth);
                    Thread.Sleep(2000);

                    CurrentPerson.CurrUser = newPerson;
                    if (CurrentPerson.CurrUser.IsBirthday)
                        MessageBox.Show("Happy birhday, buddy! Now you're closer to your death than year ago)");

                    MessageBox.Show(CurrentPerson.CurrUser.ToString());
                }
                catch (InvalidNameException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (InvalidEmailException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (NegativeAgeException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (DiedPersonException e)
                {
                    MessageBox.Show(e.Message);
                }         
            });

            _showLoaderAction.Invoke(false);
        }



        #region Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
