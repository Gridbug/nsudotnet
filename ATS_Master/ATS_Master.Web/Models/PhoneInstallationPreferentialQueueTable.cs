﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class PhoneInstallationPreferentialQueueRow
    {
        public int Id { get; set; }

        public string AtsUser { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }
    }

    public class PhoneInstallationPreferentialQueueIndexViewModel
    {
        public Configurator<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow> Table { get; set; }

        public SelectListItem[] AllAtsUsers { get; set; }
    }

    public static class PhoneInstallationPreferentialQueueTable
    {
        public static Configurator<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow> Configure(this Configurator<PhoneInstallationPreferentialQueue, PhoneInstallationPreferentialQueueRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            //configurator.Column(x => x.Id).DataOnly();

            configurator.ProjectDataWith(queues => queues.Select(queue => new PhoneInstallationPreferentialQueueRow()
            {
                Id = queue.Id,
                UserName = queue.AtsUser.Person.Name,
                UserSurname = queue.AtsUser.Person.Surname,
                UserId = queue.AtsUserId
            }));

            return configurator;
        }
    }
}