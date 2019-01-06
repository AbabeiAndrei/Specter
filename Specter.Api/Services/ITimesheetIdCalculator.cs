using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Specter.Api.Data.Repository;

namespace Specter.Api.Services
{
    public interface ITimesheetIdCalculator
    {
        int GetId(Guid projectId);
    }

    public class TimesheetIdCalculator : ITimesheetIdCalculator
    {
        private readonly ITimesheetRepository _timesheetRepository;

        private readonly IDeliveryRepository _deliveryRepository;

        private static readonly object _lock = new object();

        private int _lastNumber = -1;

        public TimesheetIdCalculator(ITimesheetRepository timesheetRepository, IDeliveryRepository deliveryRepository)
        {
            _timesheetRepository = timesheetRepository;
            _deliveryRepository = deliveryRepository;
        }

        public int GetId(Guid projectId)
        {
            var deliveries = _deliveryRepository.GetByProject(projectId)
                                                .Select(d => d.Id)
                                                .ToList();
            
            if(_lastNumber < 0)
                lock(_lock)
                    _lastNumber = _timesheetRepository.GetAll()
                                                      .Where(ts => ts.DeliveryId.HasValue && 
                                                                   deliveries.Contains(ts.DeliveryId.Value))
                                                      .Select(ts => ts.InternalId)
                                                      .DefaultIfEmpty(0)
                                                      .Max();

            return _lastNumber++;
        }
    }
}