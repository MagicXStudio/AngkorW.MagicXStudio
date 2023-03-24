using System.Diagnostics;

namespace MaterialDesign3Demo
{
    public partial class Snackbars
    {
        public Snackbars() => InitializeComponent();

        private void SnackBar3_OnClick(object sender, RoutedEventArgs e)
        {
            if (SnackbarThree.MessageQueue is { } messageQueue)
            {
                //use the message queue to send a message.
                var message = MessageTextBox.Text;
                //the message queue can be called from any thread
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
        }

        private void SnackBar4_OnClick(object sender, RoutedEventArgs e)
        {
            if (SnackbarFour.MessageQueue is { } messageQueue)
            {
                SnackbarFour.MessageQueue.DiscardDuplicates = DiscardDuplicateCheckBox.IsChecked ?? false;
                foreach (var s in ExampleFourTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    messageQueue.Enqueue(
                    s,
                    "张翰",
                    param => Trace.WriteLine("君不见吴中张翰称达生，秋风忽忆江东行: " + param),
                    s);
                }
            }

        }

        private void SnackBar4_OnClearClick(object sender, RoutedEventArgs e)
            => SnackbarFour.MessageQueue?.Clear();

        private void SnackBar7_OnClick(object sender, RoutedEventArgs e)
        {
            var duration = MessageDurationOverrideSlider.Value;
            SnackbarSeven.MessageQueue?.Enqueue(
                $"且乐生前一杯酒，何须身后千载名？ {duration:F1} .",
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(duration));
        }
    }
}
