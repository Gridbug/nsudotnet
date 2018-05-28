using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reinforced.Lattice.Commands;
using Reinforced.Lattice.Configuration;
using Reinforced.Lattice.Editing;
using Reinforced.Lattice.Editing.Editors.Memo;

namespace ATS_Master.Web.Controllers
{
    public static class MyFancyUtilities
    {
        public static CommandDescriptionConfigurator ConfigureRemoval(this CommandDescriptionConfigurator x)
        {
            x.Window<RemovalConfirmationViewModel>("confirmDelete", "#ltcModalHolder", ui =>
            {
                ui.AutoForm(_ =>
                {
                    _.EditMemo(d => d.Cause).FakeColumn(d => d.Title("Enter removal cause"));
                });
            });
            return x;
        }
    }

    class RemovalConfirmationViewModel
    {
        public string Cause { get; set; }
    }
}