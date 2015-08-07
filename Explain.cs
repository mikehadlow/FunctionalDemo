using System;

namespace FunctionalDemo
{
    public static class Explain
    {
        public static void Composition()
        {
            var dependency = new Dependency();

            var thing = new Thing(dependency);

            var relyOnThing = new RelyOnThing(thing);
        }
    }

    public interface IThing
    {
        void Do(string arg);
    }

    public class Thing : IThing
    {
        public Thing(IDependency dependency)
        {
        }

        public void Do(string arg)
        {
        }
    }

    public class RelyOnThing
    {
        public RelyOnThing(IThing thing)
        {
            
        }
    }

    public interface IDependency { }
    public class Dependency : IDependency { }

    public static class Module
    {
        public static void Composition()
        {
            var dependency = new Dependency();

            Action relyOnThing = () => RelyOnThing(arg => DoThing(dependency, arg));
        }

        public static void DoThing(IDependency dependency, string arg) { }

        public static void RelyOnThing(Action<string> thing) { }
    }
}