using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS_Master.Data.Entities;
using Reinforced.Lattice.Configuration;

namespace ATS_Master.Web.Models
{
    public class PhoneInstallationQueueRow
    {
        public int Id { get; set; }

        public string AtsUser { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }
    }

    public class PhoneInstallationQueueIndexViewModel
    {
        public Configurator<PhoneInstallationQueue, PhoneInstallationQueueRow> Table { get; set; }

        public SelectListItem[] AllAtsUsers { get; set; }
    }

    public static class PhoneInstallationQueueTable
    {
        public static Configurator<PhoneInstallationQueue, PhoneInstallationQueueRow> Configure(this Configurator<PhoneInstallationQueue, PhoneInstallationQueueRow> configurator)
        {
            configurator.DefaultTable();

            configurator.PrimaryKey(x => x.Id);

            configurator.OrderFallback(x => x.Id);

            //configurator.Column(x => x.Id).DataOnly();
            configurator.Column(row => row.UserName).DataOnly();
            configurator.Column(row => row.UserSurname).DataOnly();
            configurator.Column(row => row.UserId).DataOnly();

            configurator.ProjectDataWith(queues => queues.Select(queue => new PhoneInstallationQueueRow()
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