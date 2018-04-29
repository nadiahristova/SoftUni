namespace OnlineQuestions.Data.Migrations
{
    using OnlineQuestions.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineQuestions.Data.OnlineQuestionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OnlineQuestions.Data.OnlineQuestionsContext ctx)
        {
                if (!ctx.Tests.Any())
                {
                    var cosmologyQuestions = new List<Question>()
                {
                    new Question()
                    {
                        Content = "When astronomers look at distant galaxies, what sort of motion do they see?",
                        Answers = new List<Answer>()
                        {
                            new Answer("The Galaxies are all spinning rapidly."),
                            new Answer("The galaxies are all moving rapidly toward us."),
                            new Answer("The galaxies are all moving rapidly away from us.", true),
                            new Answer("Galaxies are falling toward three centers in opposite parts of the sky."),
                            new Answer("Galaxies to our north are moving away from us. Those to our south are approaching.")
                        }
                    },
                    new Question()
                    {
                        Content = " Compared to a stationary galaxy, light from a galaxy that is moving away from Earth will appear _____.",
                        Answers = new List<Answer>()
                        {
                            new Answer("bluer"),
                            new Answer("redder", true),
                            new Answer("the same")
                        }
                    },
                    new Question()
                    {
                        Content = " Galaxies are seen moving away from us because _____.",
                        Answers = new List<Answer>()
                        {
                            new Answer("space itself is expanding", true),
                            new Answer("the universe is considered \"open\""),
                            new Answer("Earth is at the center of the universe"),
                            new Answer("the cosmic microwave background radiation pushes them")
                        }
                    },
                    new Question()
                    {
                        Content = "Nucleosynthesis began when _____ and ended when _____.",
                        Answers = new List<Answer>()
                        {
                            new Answer("the Big Bang occurred; the Big Crunch occurred"),
                            new Answer("nuclear reactions could first occur; matter had condensed into galaxies and stars"),
                            new Answer("the cosmic microwave background radiation formed; dark energy replaced it as the dominant energy form"),
                            new Answer("the universe had cooled enough for atoms to combine; protons and neutrons were too far apart to form nuclei", true)
                        }
                    },
                    new Question()
                    {
                        Content = "Galaxies that are closer to Earth appear to be _____ Earth.",
                        Answers = new List<Answer>()
                        {
                            new Answer("older than", true),
                            new Answer("younger than"),
                            new Answer("the same age as")
                        }
                    }
                };

                    var animalKingdomQuestions = new List<Question>()
                {
                    new Question()
                    {
                        Content = "The largest animal ever existed on earth is:",
                        Answers = new List<Answer>()
                        {
                            new Answer("Woolly Mammoth"),
                            new Answer("African elephant "),
                            new Answer("Tyrannosaurus"),
                            new Answer("Sulphur bottom (blue) Whale", true)
                        }
                    },
                    new Question()
                    {
                        Content = "Which class has the largest number of animals?",
                        Answers = new List<Answer>()
                        {
                            new Answer("Mammals"),
                            new Answer("Fishes"),
                            new Answer("Insects", true),
                            new Answer("Reptiles")
                        }
                    }
                };

                    var tests = new List<Test>()
                {
                    new Test()
                    {
                        Title = "Quiz on Cosmology",
                        Questions = cosmologyQuestions
                    },
                    new Test()
                    {
                        Title = "Animal kingdom Quiz",
                        Questions = animalKingdomQuestions
                    },
                    new Test()
                    {
                        Title = "Mixed Quiz",
                        Questions = new List<Question>()
                    },
                };

                    ctx.Tests.AddRange(tests);
                    ctx.SaveChanges();

                    var allQuestions = ctx.Tests.SelectMany(t => t.Questions);
                    foreach (var q in allQuestions)
                    {
                        ctx.Tests.FirstOrDefault(t => t.Title == "Mixed Quiz").Questions.Add(q);
                    }

                    ctx.SaveChanges();
                }
        }
    }
}
