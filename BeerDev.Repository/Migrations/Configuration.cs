using BeerDev.Entities;

namespace BeerDev.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BeerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BeerDbContext context)
        {
            //context.Beers.AddOrUpdate(b => b.Code,
            //    new Beer
            //    {
            //        Name = "Pilsener Urquell",
            //        Description = "While most lagers .",
            //        Code = "PUR",
            //        Alchool = 4.4M,
            //        Kind = "Pilsen - Bohemian Pilsner",
            //        Nationality = "Czech",
            //        Picture = "pilsener_urquell.jpg",
            //        Price = 10
            //    },
            //    new Beer
            //    {
            //        Name = "Stella Artois",
            //        Description = ".",
            //        Code = "SAR",
            //        Alchool = 5.2M,
            //        Kind = "Lager - Belguim Lager",
            //        Nationality = "Belguim",
            //        Picture = "stella.png",
            //        Price = 3.49M
            //    }
            //    ,
            //    new Beer
            //    {
            //        Name = "Corona Extra",
            //        Description = "Corona delivers a unique fun, ...",
            //        Code = "COE",
            //        Alchool = 4.6M,
            //        Kind = "Premium American Lager",
            //        Nationality = "Mexican",
            //        Picture = "corona.jpg",
            //        Price = 6
            //    },
            //    new Beer
            //    {
            //        Name = "Paulistania",
            //        Description = "The new national malt beer...",
            //        Code = "SPB",
            //        Alchool = 4.8M,
            //        Kind = "Premium American Lager",
            //        Nationality = "Brazilian",
            //        Picture = "paulistania.jpg",
            //        Price = 9
            //    },
            //    new Beer
            //    {
            //        Name = "Austria Pilsen",
            //        Description = "Beer type Pilsen American Lager light...",
            //        Code = "API",
            //        Alchool = 4.5M,
            //        Kind = "Premium American Lager",
            //        Nationality = "Brazilian",
            //        Picture = "austria.jpg",
            //        Price = 7
            //    },
            //    new Beer
            //    {
            //        Name = "Hofbräu Original",
            //        Description = "More than any other, Hofbräu Original ...",
            //        Code = "HFB",
            //        Alchool = 5.1M,
            //        Kind = "Pilsen - Munich Helles",
            //        Nationality = "German",
            //        Picture = "hb.jpg",
            //        Price = 9.71M
            //    }
            //    );

            context.PictureGalleries.AddOrUpdate(p => p.Caption,
                new PictureGallery
                {
                    Picture = "Images/pilsener_urquell.bmp",
                    Caption = "Pilsener Urquell - Czech Bohemian Pilsner"
                },
                new PictureGallery
                {
                    Picture = "Images/stella.png",
                    Caption = "Stella Artois - Belguim Lager"
                },
                new PictureGallery
                {
                    Picture = "Images/corona.jpg",
                    Caption = "Corona Extra - Mexican Lager"
                },
                new PictureGallery
                {
                    Picture = "Images/austria.jpg",
                    Caption = "Austria Pilsen - Brazilian Lager"
                },
                new PictureGallery
                {
                    Picture = "Images/paulistania.jpg",
                    Caption = "Paulistania - Brazilian Lager"
                },
                new PictureGallery
                {
                    Picture = "Images/hb.jpg",
                    Caption = "Hofbräu Original - German Pilsner"
                },
                new PictureGallery
                {
                    Picture = "http://i.imgur.com/ujNADWn.jpg",
                    Caption = "Hot German Girl"
                },
                new PictureGallery
                {
                    Picture = "http://i.imgur.com/ScxhJ1r.jpg",
                    Caption = "Even the dogs likes it!"
                });

            context.Profiles.AddOrUpdate(p=>p.CustomerCategory, 
                new Profile
                {
                    CustomerCategory = "gold",
                    BuyingStyle = "same",
                    InvoiceAverage = "high"
                });
        }
    }
}
