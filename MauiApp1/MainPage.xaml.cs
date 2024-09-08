namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        string translatedNumber; // Хранит переведенный номер

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text; // Получаем введенный номер
            translatedNumber = MauiApp1.PhonewordTranslator.ToNumber(enteredNumber); // Переводим номер

            // Включаем/отключаем кнопку "Call" в зависимости от результата перевода:
            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";
            }
        }

        // Обработчик события нажатия кнопки "Call":
        async void OnCall(object sender, System.EventArgs e)
        {
            // Вывод диалогового окна с подтверждением:
            if (await this.DisplayAlert("Dial a Number", "Would you like to call " + translatedNumber + "?", "Yes", "No"))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                        PhoneDialer.Default.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (Exception)
                {
                    // Other error has occurred.
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
    }
}