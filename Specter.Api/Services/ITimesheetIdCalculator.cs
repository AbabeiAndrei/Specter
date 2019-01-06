using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Specter.Api.Data.Repository;

namespace Specter.Api.Services
{
    public interface ITimesheetIdCalculator
    {
        int Calculate(Guid projectId);
    }

    public class TimesheetIdCalculator : ITimesheetIdCalculator
    {
        private readonly ITimesheetRepository _timesheetRepository;

        private readonly IDeliveryRepository _deliveryRepository;

        private static readonly object _lock = new object();

        private static int _lastNumber = -1;

        public TimesheetIdCalculator(ITimesheetRepository timesheetRepository, IDeliveryRepository deliveryRepository)
        {
            _timesheetRepository = timesheetRepository;
            _deliveryRepository = deliveryRepository;
        }

        public int Calculate(Guid projectId)
        {
            
            lock(_lock)
            {
                if(_lastNumber < 0)
                    _lastNumber = _timesheetRepository.GetAll()
                                                        .Where(ts => ts.Delivery.ProjectId == projectId)
                                                        .Select(ts => ts.InternalId)
                                                        .DefaultIfEmpty(0)
                                                        .Max();

                return ++_lastNumber;
            }
        }
    }
}