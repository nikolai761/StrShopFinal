using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StrShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrShop.Data
{
    public class DbObject
    {
        public static void initial(DBconnection content)
        {
            if(!content.Category.Any())
            {
                content.Category.AddRange(Categories.Select(c=>c.Value));
            }

            if (!content.Producer.Any())
            {
                content.Producer.AddRange(Producer.Select(c => c.Value));
            }

            if (!content.Storage.Any())
            {
                content.Storage.AddRange(Storage.Select(c => c.Value));
            }

            if (!content.Item.Any())
            {
                content.AddRange(
                    new Item
                    {
                        ItemName = "Краска Белая",
                        Description = "Краска для стен",
                        price = 7,
                        Category = Categories["Краски"],
                        Producer = Producer["Sniezka"],
                        Storage = Storage["Уручье"],
                        isAction = true,
                        img = "/img/Sniezka.jpg"

                    },
                    new Item
                    {
                        ItemName = "Штукатурка",
                        Description = "Бюджетная",
                        price = 15,
                        Category = Categories["Смеси"],
                        Producer = Producer["Sniezka"],
                        Storage = Storage["Уручье"],
                        isAction = true,
                        img = "/img/Sniezka.jpg"

                    },
                    new Item
                    {
                        ItemName = "Краска Красная",
                        Description = "По металлу ",
                        price = 11,
                        Category = Categories["Краски"],
                        Producer = Producer["Condor"],
                        Storage = Storage["Уручье"],
                        isAction = false,
                        img = "/img/Condor.jpg"

                    },
                    new Item
                    {
                        ItemName = "Лак для пола",
                        Description = "Сверхпрочный ",
                        price = 15,
                        Category = Categories["Лаки"],
                        Producer = Producer["Farbitex"],
                        Storage = Storage["Уручье"],
                        isAction = true,
                        img = "/img/lak.jpg"

                    },
                     new Item
                     {
                         ItemName = "Лак по дереву",
                         Description = "долговечный ",
                         price = 20,
                         Category = Categories["Лаки"],
                         Producer = Producer["Farbitex"],
                         Storage = Storage["Уручье"],
                         isAction = false,
                         img = "/img/lakd.jpg"

                     },
                     new Item
                     {
                         ItemName = "Желтая краска",
                         Description = "Влагостойкая ",
                         price = 17,
                         Category = Categories["Краски"],
                         Producer = Producer["Farbitex"],
                         Storage = Storage["Уручье"],
                         isAction = true,
                         img = "/img/Yel.jpg"

                     },
                     new Item
                     {
                         ItemName = "Брус",
                         Description = "50 ",
                         price = 1,
                         Category = Categories["Пиломатериалы"],
                         Producer = Producer["Les"],
                         Storage = Storage["Уручье"],
                         isAction = false,
                         img = "/img/brus.jpg"

                     },
                     new Item
                     {
                         ItemName = "Балка",
                         Description = "Дуб ",
                         price = 5,
                         Category = Categories["Пиломатериалы"],
                         Producer = Producer["Drova"],
                         Storage = Storage["Уручье"],
                         isAction = false,
                         img = "/img/balka.jpg"

                     },
                      new Item
                      {
                          ItemName = "Колышки",
                          Description = "нестроганные ",
                          price = 1,
                          Category = Categories["Пиломатериалы"],
                          Producer = Producer["Drova"],
                          Storage = Storage["Уручье"],
                          isAction = true,
                          img = "/img/kol.jpg"

                      },

                     new Item
                     {
                         ItemName = "Лист металлический",
                         Description = "0.5 ",
                         price = 7,
                         Category = Categories["Металлопрокат"],
                         Producer = Producer["Metallica"],
                         Storage = Storage["Уручье"],
                         isAction = false,
                         img = "/img/listmet.jpg"

                     },
                      new Item
                      {
                          ItemName = "Труба металлическая",
                          Description = "круглая ",
                          price = 10,
                          Category = Categories["Металлопрокат"],
                          Producer = Producer["Metallica"],
                          Storage = Storage["Уручье"],
                          isAction = true,
                          img = "/img/trubak.jpg"

                      },
                      new Item
                      {
                          ItemName = "Труба металлическая",
                          Description = "Прямоугольная ",
                          price = 10,
                          Category = Categories["Металлопрокат"],
                          Producer = Producer["Metallica"],
                          Storage = Storage["Уручье"],
                          isAction = false,
                          img = "/img/trubap.jpg"

                      },
                      new Item
                      {
                          ItemName = "Арматура",
                          Description = "20 мм ",
                          price = 5,
                          Category = Categories["Металлопрокат"],
                          Producer = Producer["Metallica"],
                          Storage = Storage["Уручье"],
                          isAction = false,
                          img = "/img/arma.jpg"

                      },
                      new Item
                      {
                          ItemName = "Арматура",
                          Description = "30 мм ",
                          price = 7,
                          Category = Categories["Металлопрокат"],
                          Producer = Producer["Metallica"],
                          Storage = Storage["Уручье"],
                          isAction = true,
                          img = "/img/arma.jpg"

                      }


                    );
            }
            content.SaveChanges();
        }

        private static Dictionary<string, Producer> producer;
        public static Dictionary<string, Producer> Producer
        {
            get{
                if (producer == null) {
                    var list = new Producer[]
                    {
                        new  Producer{  ProducerName ="Sniezka"},
                        new  Producer{  ProducerName ="Condor"},
                        new  Producer{  ProducerName ="Farbitex"},
                        new  Producer{  ProducerName ="Les"},
                        new  Producer{  ProducerName ="Metallica"},
                        new  Producer{  ProducerName ="Drova"}
                    };

                    producer = new Dictionary<string, Producer>();
                    foreach (Producer el in list) { producer.Add(el.ProducerName, el); }
                }
                return producer;
            }
           
        }

        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new Category[]
                    {
                        new Category{ CategoryName ="Смеси"},
                        new Category{ CategoryName ="Краски"},
                         new Category{ CategoryName ="Лаки"},
                         new Category{ CategoryName ="Пиломатериалы"},
                         new Category{ CategoryName ="Металлопрокат"},
                    };

                    category = new Dictionary<string, Category>();
                    foreach (Category el in list) { category.Add(el.CategoryName, el); }
                }
                return category;
            }

        }

        private static Dictionary<string, Storage> storage;
        public static Dictionary<string, Storage> Storage
        {
            get
            {
                if (storage == null)
                {
                    var list = new Storage[]
                    {
                        new Storage{ StorageAddress ="Уручье"},
                        new Storage{ StorageAddress ="Зеленый луг"},
                    };

                    storage = new Dictionary<string, Storage>();
                    foreach (Storage el in list) { storage.Add(el.StorageAddress, el); }
                }
                return storage;
            }

        }
    }
}
