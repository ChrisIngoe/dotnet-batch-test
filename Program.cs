using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp
{
    class Rule
    {
        public int DelayTime { get; set; }
    }

    class Batch
    {
        public int RunId { get; set; }
        public async Task<bool> Commit(int delayTime)
        {
            // Simulate 'doing stuff'
            await Task.Delay(delayTime);
            return true;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            int batchCounter = 1;
            List<Rule> businessRules = GetBusinessRules();

            foreach (var businessRule in businessRules)
            {
                Batch batch = new Batch();

                batch.RunId = batchCounter;

                //Get the batch to commit some stuff. (Parameter is just a way to simulate how long the batch migtht run for)
                await batch.Commit(businessRule.DelayTime);

                //Run an end task on another thread pool
                await Task.Run(() => Console.WriteLine("completed batch " + batchCounter + " delay time = " + businessRule.DelayTime));
                batchCounter++;
            }
        }

        static List<Rule> GetBusinessRules()
        {
            List<Rule> businessRules = new List<Rule>();
            businessRules.Add(new Rule { DelayTime = 3000 });
            businessRules.Add(new Rule { DelayTime = 2000 });
            businessRules.Add(new Rule { DelayTime = 1000 });
            return businessRules;
        }
    }
}