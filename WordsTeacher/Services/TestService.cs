using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.Data;
using WordsTeacher.Data.Entities;

namespace WordsTeacher.Services
{
    public class TestService
    {
        private readonly Repository<Test> _testRepository;

        public TestService(Repository<Test> testRepository)
        {
            _testRepository = testRepository;
        }

        public Test GetById(int id)
        {
            return _testRepository.Table.FirstOrDefault(a => a.Id == id);
        }

        public List<Test> GetTests()
        {
            return _testRepository.Table.ToList();
        }

        public Test Insert(Test test)
        {
            if (test == null)
                return null;

            return _testRepository.Insert(test);
        }

        public Test Update(Test test)
        {
            if (test == null)
                return null;

            return _testRepository.Update(test);
        }

        public void Delete(Test test)
        {
            if (test == null)
                return;

            _testRepository.Delete(test);
        }
    }
}