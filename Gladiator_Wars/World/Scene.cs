using System.Collections;


namespace Gladiator_Wars
{
    internal class Scene
    {
        private ArrayList _sceneComponents;

        public Scene()
        {
            _sceneComponents = new ArrayList();
        }

        public void addItem(GameObject newComponent)
        {
            _sceneComponents.Add(newComponent);
        }

        public void removeItem(GameObject componentToBeRemoved)
        {
            _sceneComponents.Remove(componentToBeRemoved);
        }

        public ArrayList getSceneComponents()
        {
            return _sceneComponents;
        }

        public void clear()
        {
            _sceneComponents.Clear();
        }
    }
}
