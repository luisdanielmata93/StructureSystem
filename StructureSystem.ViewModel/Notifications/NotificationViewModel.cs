using System;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using Notifications.Wpf;
using StructureSystem.Model;

namespace StructureSystem.ViewModel
{
    public class NotificationViewModel 
    {
        private INotificationManager _manager;

        public string Title { get; set; }
        public string Message { get; set; }

        public NotificationViewModel()
        {
            try
            {
                this._manager = new NotificationManager();
            }
            catch (Exception ex)
            {

            }
        }

        public async void ShowNotification(OperationResult data)
        {
            try
            {
                await Task.Delay(300);

                if (!data.Error)
                    _manager.Show(new NotificationContent { Title = "Notificación", Message = data.Message, Type = data.NotificationType }, areaName: "WindowArea");
                else
                    _manager.Show(new NotificationContent { Title = "Error!", Message = data.Message, Type =data.NotificationType}, areaName: "WindowArea");

            }
            catch (Exception ex)
            {

            }
            
        }



    }//end of class
}//end of namespace
