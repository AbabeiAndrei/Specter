#if DEBUG 

using System;
using System.Linq;

using Microsoft.AspNetCore.Identity;

using Specter.Api.Data.Entities;
using Specter.Api.Data.Repository;

namespace Specter.Api.Services
{
    public interface ITestDataSeeder 
    {
        bool CanSeed { get; }

        void SeedTestData();
    }

    public class TestDataSeeder : ITestDataSeeder
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITimesheetIdCalculator _timesheetIdCalculator;

        #endregion

        #region Properties

        public bool CanSeed => !_projectRepository.GetAll().Any();

        #endregion

        #region Constructors

        public TestDataSeeder(ICategoryRepository categoryRepository, 
                              IProjectRepository projectRepository,
                              IDeliveryRepository deliveryRepository,
                              ITimesheetRepository timesheetRepository,
                              UserManager<ApplicationUser> userManager,
                              ITimesheetIdCalculator timesheetIdCalculator)
        {
            _categoryRepository = categoryRepository;
            _projectRepository = projectRepository;
            _deliveryRepository = deliveryRepository;
            _timesheetRepository = timesheetRepository;
            _userManager = userManager;
            _timesheetIdCalculator = timesheetIdCalculator;
        }

        #endregion

        #region Public methods

        public void SeedTestData()
        {
            
            #region Categories

            var cat1 = new Category
            {
                Name = "Elaboration",
                Description = "Elaboration phase of task"
            };
            var cat2 = new Category
            {
                Name = "Construction",
                Description = "Construction phase of task"
            };
            var cat3 = new Category
            {
                Name = "Testing",
                Description = "Testing phase of task"
            };
            var cat4 = new Category
            {
                Name = "Other",
                Description = "Other phase of task"
            };

            _categoryRepository.Insert(cat1);
            _categoryRepository.Insert(cat2);
            _categoryRepository.Insert(cat3);
            _categoryRepository.Insert(cat4);

            #endregion

            #region Projects

            var proj1 = new Project
            {
                Name = "Minecraft",
                Code = "MCRAFT",
                Description = "New frontend for SICLID",
                WorkItemIdPrefix = "MC"
            };
            
            var proj2 = new Project
            {
                Name = "Jetix",
                Code = "JETIX",
                Description = "Current frontend for SICLID",
                WorkItemIdPrefix = "JTX"
            };
            
            var proj3 = new Project
            {
                Name = "Other",
                Code = "INTERNAL",
                Description = "Other projects or work related items",
                WorkItemIdPrefix = "IBM"
            };

            _projectRepository.Insert(proj1);
            _projectRepository.Insert(proj2);
            _projectRepository.Insert(proj3);

            #endregion

            #region Deliveries

            var del1 = new Delivery
            {
                Name = "Minecraft 1.0",
                Order = 0,
                ProjectId = proj1.Id
            };
            var del2 = new Delivery
            {
                Name = "Minecraft 2.0",
                Order = 1,
                ProjectId = proj1.Id
            };
            var del3 = new Delivery
            {
                Name = "Minecraft 2.1",
                Order = 2,
                ProjectId = proj1.Id
            };
            var del4 = new Delivery
            {
                Name = "Minecraft 3.0",
                Order = 3,
                ProjectId = proj1.Id
            };
            var del5 = new Delivery
            {
                Name = "Jetix 2.1",
                Order = 0,
                ProjectId = proj2.Id
            };
            var del6 = new Delivery
            {
                Name = "Jetix 2.2",
                Order = 1,
                ProjectId = proj2.Id
            };

            _deliveryRepository.Insert(del1);
            _deliveryRepository.Insert(del2);
            _deliveryRepository.Insert(del3);
            _deliveryRepository.Insert(del4);
            _deliveryRepository.Insert(del5);
            _deliveryRepository.Insert(del6);

            #endregion

            #region TestTs

            var userId = _userManager.Users.First().Id;

            var ts1 = new Timesheet
            {
                InternalId = _timesheetIdCalculator.Calculate(proj1.Id),
                Name = "TestTs1Jetix",
                Description = "Test Timesheet 2 Jetix",
                Date = DateTime.Now,
                Time = 5,
                UserId = userId,
                CategoryId = cat1.Id,
                ProjectId = proj1.Id,
                DeliveryId = del1.Id
            };

            var ts2 = new Timesheet
            {
                InternalId = _timesheetIdCalculator.Calculate(proj1.Id),
                Name = "TestTs2Jetix",
                Description = "Test Timesheet 2 Jetix",
                Date = DateTime.Now,
                Time = 1,
                UserId = userId,
                CategoryId = cat1.Id,
                ProjectId = proj1.Id,
                DeliveryId = del1.Id
            };

            var ts3 = new Timesheet
            {
                InternalId = _timesheetIdCalculator.Calculate(proj1.Id),
                Name = "TestTs3Jetix",
                Description = "Test Timesheet 3 Jetix",
                Date = DateTime.Now,
                Time = 2,
                UserId = userId,
                CategoryId = cat1.Id,
                ProjectId = proj1.Id,
                DeliveryId = del1.Id
            };

            var ts4 = new Timesheet
            {
                InternalId = _timesheetIdCalculator.Calculate(proj1.Id),
                Name = "TestTs4Jetix",
                Description = "Test Timesheet 4 Jetix",
                Date = DateTime.Now.AddDays(1),
                Time = 2,
                UserId = userId,
                CategoryId = cat1.Id,
                ProjectId = proj1.Id,
                DeliveryId = del1.Id
            };

            _timesheetRepository.Insert(ts1);
            _timesheetRepository.Insert(ts2);
            _timesheetRepository.Insert(ts3);
            _timesheetRepository.Insert(ts4);

            #endregion
        }

        #endregion
    }
}

#endif