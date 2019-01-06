namespace Specter.Api.Exceptions
{
    public static class Errors
    {
        public enum Timesheet : short
        {
            TimesheetIsLocked = 1,
            DeliveryOrProjectNotProvided = 2
        }        
    }
}