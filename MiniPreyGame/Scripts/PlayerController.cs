using KokoEngine;

namespace MiniPreyGame
{
    class PlayerController : Player
    {
        public GameController GameController;
        public Texture2D CursorTexture25;
        public Texture2D CursorTexture50;
        public Texture2D CursorTexture75;
        public Texture2D CursorTexture100;

        //private LineRenderer _lineRenderer;
        private Structure _selectedStructure;
        private Structure _lastHoveredStructure;

        private float _selectedPercentage = 0.25f;

        void Awake()
        {
            //_lineRenderer = GetComponent<LineRenderer>();
            UpdateMouseTexture();
        }

        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                // Get hovered structure
                Structure structure = hit.transform.GetComponent<Structure>();

                if (structure)
                {
                    _lastHoveredStructure = structure;

                    if (structure.Owner == this)
                    {
                        // Show hover
                        if (!_selectedStructure || (_selectedStructure && _selectedStructure != structure))
                        {
                            structure.HoverOutline.SetActive(true);
                            structure.IsHovered = true;
                        }

                        // Select structure
                        if (Input.GetMouseButtonUp(0))
                        {
                            Debug.Log("Selected structure");
                            if (_selectedStructure)
                                _selectedStructure.SelectedOutline.SetActive(false);

                            _selectedStructure = structure;
                            _selectedStructure.SelectedOutline.SetActive(true);
                            _selectedStructure.HideUpgradeMenu();
                            UnHover();
                        }

                        if (_selectedStructure && _selectedStructure == structure)
                        {
                            // Toggle upgrade menu
                            if (Input.GetMouseButtonDown(1))
                            {
                                structure.ToggleUpgradeMenu();
                            }
                        }
                    }

                    if (_selectedStructure && _selectedStructure != structure)
                    {
                        // Show line
                        //Vector3 dir = transform.InverseTransformDirection(structure.transform.position - transform.position).normalized;

                        _lineRenderer.positionCount = 2;
                        _lineRenderer.SetPosition(0, _selectedStructure.transform.position);
                        _lineRenderer.SetPosition(1, structure.transform.position);

                        if (Input.GetMouseButtonDown(1) && _selectedStructure)
                        {
                            Debug.Log("Selected target structure");
                            _selectedStructure.LaunchCitizens(structure, _selectedPercentage);
                            _selectedStructure.SelectedOutline.SetActive(false);
                            _selectedStructure.HideUpgradeMenu();
                            _selectedStructure = null;
                            UnHover();
                        }
                    }
                }
                else
                {
                    UnHover();

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (_selectedStructure)
                        {
                            _selectedStructure.SelectedOutline.SetActive(false);
                            _selectedStructure.HideUpgradeMenu();
                            _selectedStructure = null;
                        }
                    }
                }
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                _selectedPercentage += 0.25f;
                if (_selectedPercentage > 1)
                    _selectedPercentage = 1;
                UpdateMouseTexture();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                _selectedPercentage -= 0.25f;
                if (_selectedPercentage < 0.25f)
                    _selectedPercentage = 0.25f;
                UpdateMouseTexture();
            }
        }

        void UnHover()
        {
            //_lineRenderer.positionCount = 0;

            if (_lastHoveredStructure)
            {
                _lastHoveredStructure.HoverOutline.SetActive(false);
                _lastHoveredStructure.IsHovered = false;
            }

            if (_selectedStructure)
            {
                _selectedStructure.HoverOutline.SetActive(false);
                _selectedStructure.IsHovered = false;
            }
        }

        void UpdateMouseTexture()
        {
            if (_selectedPercentage == 0.25f)
                Cursor.SetCursor(CursorTexture25, Vector2.zero, CursorMode.Auto);
            else if (_selectedPercentage == 0.5f)
                Cursor.SetCursor(CursorTexture50, Vector2.zero, CursorMode.Auto);
            else if (_selectedPercentage == 0.75f)
                Cursor.SetCursor(CursorTexture75, Vector2.zero, CursorMode.Auto);
            else if (_selectedPercentage == 1f)
                Cursor.SetCursor(CursorTexture100, Vector2.zero, CursorMode.Auto);
        }
    }
}
