using System;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using Notifications.Wpf;
using StructureSystem.Model;

namespace StructureSystem.ViewModel
{
    public class NotificationViewModel : PropertyChangedBase
    {
        private readonly INotificationManager _manager;

        public string Title { get; set; }
        public string Message { get; set; }

        public NotificationViewModel(INotificationManager manager)
        {
            _manager = manager;
        }

        public async void Ok()
        {
            await Task.Delay(500);
            _manager.Show(new NotificationContent { Title = "Success!", Message = "Ok button was clicked.", Type = NotificationType.Success });
        }

        public async void Cancel()
        {
            await Task.Delay(500);
            _manager.Show(new NotificationContent { Title = "Error!", Message = "Cancel button was clicked!", Type = NotificationType.Error });
        }

        public async void ShowNotification(OperationResult data)
        {
            if (data.Code == "3")
            {
                _manager.Show(new NotificationContent { Title = "Notificación", Message = data.Message, Type = NotificationType.Warning }, areaName: "WindowArea");
            }

            if (!data.Error)
                _manager.Show(new NotificationContent { Title = "Notificación", Message = data.Message, Type = NotificationType.Success }, areaName: "WindowArea");
            else
                _manager.Show(new NotificationContent { Title = "Error", Message = data.Message, Type = NotificationType.Error }, areaName: "WindowArea");

        }



    }//end of class
}//end of namespace
