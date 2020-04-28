#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion


namespace TopDownShooterProject2020
{
    public class SquareGrid
    {
        
        

        public bool showGrid;

        public Basic2d gridImage;

        public Vector2 slotDimensions, gridDimansions, physicalStartPosition, totalPhysicalDimensions, currentHoverSlot ;

        public List<GridItem> gridItems = new List<GridItem>();
        public List<List<GridLocation>> slots = new List<List<GridLocation>>();

        #region Constructors
        public SquareGrid(Vector2 slotDimensions, Vector2 startPosition, Vector2 totalDimensions, XElement data)
        {
            showGrid = false;

            this.slotDimensions = slotDimensions;

            physicalStartPosition = new Vector2((int)startPosition.X, (int)startPosition.Y);
            totalPhysicalDimensions = new Vector2((int)totalDimensions.X, (int)totalDimensions.Y);


            currentHoverSlot = new Vector2(-1, -1); //Basically saying we are not hovering on anything

            SetBaseGrid();


            gridImage = new Basic2d("2d\\Misc\\shade", this.slotDimensions/2, new Vector2(this.slotDimensions.X-2, this.slotDimensions.Y-2));



            LoadData(data); 
        }
        #endregion


        #region Methods

        // Updates where we are currently hovering every frame
        public virtual void Update(Vector2 offset)
        {
            currentHoverSlot = GetSlotFromPixel(Globals.mouse.newMousePosition, -offset); // negative offset becaus the grid works in the opposite direction then the offset moves
        }

        public virtual GridLocation GetSlotFromLocation(Vector2 location)
        {
            if(location.X >= 0 && location.Y >= 0 && location.X < slots.Count && location.Y < slots[(int)location.X].Count)
            {
                return slots[(int)location.X][(int)location.Y];
            }

            return null;
        }


        public virtual List<GridLocation> GetSlotsFromLocationAndSize(Vector2 location, Vector2 size)
        {
            List<GridLocation> tempLocations = new List<GridLocation>();
            location = new Vector2(location.X - (int)(size.X/2), location.Y - (int)(size.Y/2));          

            if (location.X >= 0 && location.Y >= 0 && location.X < slots.Count && location.Y < slots[(int)location.X].Count)
            {
                for (int i = 0; i < size.X; i++)
                {
                    for (int j = 0; j < size.Y; j++)
                    {
                       if ((int)location.X + i >= slots.Count || (int)location.Y + j >= slots[0].Count) return null;
                        tempLocations.Add(slots[(int)location.X + i][(int)location.Y + j]);
                    }
                }

                return tempLocations;
            }
            return null;
        } 


        public virtual bool CheckBlockImpassable(List<GridLocation> locations)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                if (GetSlotFromLocation(locations[i].position).impassable) return true;
            }
            return false;
        }
        public virtual bool CheckBlockFilled(List<GridLocation> locations)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                if (locations[i].filled) return true;
            }
            return false;
        }
        public virtual void FillBlock(List<GridLocation> locations)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                locations[i].SetToFilled(false);
            }
        }
        public virtual void UnFillBlock(List<GridLocation> locations)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                locations[i].SetToNotFilled(false);
            }
        }

        public virtual GridLocation GetPrevSlotFromLocation(Vector2 location)
        {
            if (location.X >= 0 && location.Y >= 0 && location.X < slots.Count && location.Y < slots[(int)location.X].Count)
            {
                return slots[(int)location.X - 1][(int)location.Y];
            }

            return null;
        }
        #endregion
        public virtual Vector2 GetSlotFromPixel(Vector2 pixel, Vector2 offset)
        {
            Vector2 adjustedPosition = pixel - physicalStartPosition + offset;

            Vector2 tempVector = new Vector2(Math.Min(Math.Max(0, (int)(adjustedPosition.X / slotDimensions.X)), slots.Count-1), Math.Min(Math.Max(0, (int)(adjustedPosition.Y / slotDimensions.Y)), slots[0].Count - 1));

            return tempVector;
        }

        public virtual void AddGridItem(string path, Vector2 location) // Basic witht the slot dimansions
        {
            gridItems.Add(new GridItem(path, GetPositionFromLocation(location) + slotDimensions/2, new Vector2(slotDimensions.X,slotDimensions.Y)/2, new Vector2(1, 1)));
            GetSlotFromLocation(location).SetToFilled(true);
        }
        
        // Returns the top left of the location, just add the slot dimensions to get to the middle
        public virtual Vector2 GetPositionFromLocation(Vector2 location)
        {
            return physicalStartPosition + new Vector2((int)location.X * slotDimensions.X, (int)location.Y * slotDimensions.Y);
        }

        public virtual void LoadData(XElement data)
        {
            if(data != null)
            {
                List<XElement> gridItemsList = (from t in data.Descendants("GridItem")
                                            select t).ToList<XElement>();

                for (int i = 0; i< gridItemsList.Count; i++)
                {
                    AddGridItem("2d\\Misc\\solid", new Vector2(Convert.ToInt32(gridItemsList[i].Element("Location").Element("x").Value, Globals.culture), Convert.ToInt32(gridItemsList[i].Element("Location").Element("y").Value, Globals.culture)));
                }
            }
        }

        // Grid innitialization, creating a big empty grid
        public virtual void SetBaseGrid()   
        {
            // Taking the total physical dimensions devided by the size of the slots to get the number of slots
            gridDimansions = new Vector2((int)(totalPhysicalDimensions.X / slotDimensions.X), (int)(totalPhysicalDimensions.Y / slotDimensions.Y));

            slots.Clear();

            for(int i = 0; i<gridDimansions.X; i++)
            {
                slots.Add(new List<GridLocation>()); // Creates new column

                for(int j = 0; j<gridDimansions.Y; j++)
                {
                    if(i == 0 || i == gridDimansions.X-1 || j==0 || j == gridDimansions.Y-1) slots[i].Add(new GridLocation(1, true));
                    else slots[i].Add(new GridLocation(1, false)); // Nothing in the grid

                }
            }
        }

        #region A* algorithem
        public List<Vector2> GetPath(Vector2 start, Vector2 end, bool allowDiagnals)
        {
            // Creating vewable and used lists, and the master grid which is basically a copy of our grid so we can make changes to is without actually make changes to our grid
            List<GridLocation> viewable = new List<GridLocation>(), used = new List<GridLocation>();
            List<List<GridLocation>> masterGrid = new List<List<GridLocation>>();

            #region Copy the grid to the master grid
            bool impassable = false;
            float cost = 1;
            for(int i = 0; i < slots.Count; i++)
            {
                masterGrid.Add(new List<GridLocation>());
                for(int j = 0; j<slots[i].Count; j++)
                {
                    impassable = slots[i][j].impassable;
                    if(slots[i][j].impassable || slots[i][j].filled)
                    {
                        impassable = true;
                    }

                    cost = slots[i][j].cost;
                    masterGrid[i].Add(new GridLocation(new Vector2(i, j), cost, impassable, 99999999)); // Position in grid space not screen space, fscore so high so it will push it to last (0 can casue bugs)
                }
            }
            #endregion

            // We need to know where the start is so the start node is automatically viewable   
            viewable.Add(masterGrid[(int)start.X][(int)start.Y]);

            // As long as viewable > 0 (theres a place to go) and viewable != end position (we cant see the end)
            while(viewable.Count > 0 && !(viewable[0].position.X == end.X && viewable[0].position.Y == end.Y))
            {
                // Running test Astar node
                TestAStarNode(masterGrid, viewable, used, end, allowDiagnals);
            }

            // After test A* node is done we have a path to go through
            List<Vector2> path = new List<Vector2>();

            // The viewable node thats in the front of viewable is our end point and if  viewable.count equals to 0 we didnt find a path, if its greater the 0 we have a path  
            if(viewable.Count > 0)
            {
                int currentViewableStart = 0;
                GridLocation currentNode = viewable[currentViewableStart];

                path.Clear();
                Vector2 tempPosition;

                // Creating the path, going from the end backwards with parent until the position is the start position
                while(true)
                {
                    // Adds the differences between the actual grid and the custom grid back in... (move it from grid space to game space)
                    tempPosition = GetPositionFromLocation(currentNode.position) + slotDimensions / 2;
                    path.Add(new Vector2(tempPosition.X, tempPosition.Y));

                    if(currentNode.position == start)
                    {
                        break;
                    }
                    else
                    {
                        currentNode = masterGrid[(int)currentNode.parent.X][(int)currentNode.parent.Y];
                    }
                }

                // We started at the end so we want to reverse the path
                path.Reverse();

                if(path.Count > 1) // If it has more then 1 node in the path we remove the first one so it wont go back to the first one and then to the end and create like a wierd walk back at the first step
                {
                    path.RemoveAt(0);
                }
            }
            return path;
        }

        // Gets our grid, our viewable list, our used list, the end point we are looking for and if diagnals are allowed and finds Viewable nodes and sets them
        public void TestAStarNode(List<List<GridLocation>> masterGrid, List<GridLocation> viewable, List<GridLocation> used, Vector2 end, bool allowDiagnals)
        {
            GridLocation currentNode;
            bool up = true, down = true, left = true, right = true;

            // Above, checking if theres a node above and if its passable, same for all the rest just each one is for other side
            if(viewable[0].position.Y > 0 && viewable[0].position.Y < masterGrid[0].Count && !masterGrid[(int)viewable[0].position.X][(int)viewable[0].position.Y-1].impassable)
            {
                // As long as the location is tested and it exists and its not impassable, we set the current node to that and setting the up to the same impassaness as this
                currentNode = masterGrid[(int)viewable[0].position.X][(int)viewable[0].position.Y - 1];
                up = currentNode.impassable;
                // Setting the A* Node
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, 1);  
            }

            // Below
            if (viewable[0].position.Y >= 0 && viewable[0].position.Y + 1 < masterGrid[0].Count && !masterGrid[(int)viewable[0].position.X][(int)viewable[0].position.Y + 1].impassable)
            {
                currentNode = masterGrid[(int)viewable[0].position.X][(int)viewable[0].position.Y + 1];
                down = currentNode.impassable;
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, 1);
            }

            // Left
            if (viewable[0].position.X > 0 && viewable[0].position.X < masterGrid.Count && !masterGrid[(int)viewable[0].position.X - 1][(int)viewable[0].position.Y].impassable)
            {
                currentNode = masterGrid[(int)viewable[0].position.X - 1][(int)viewable[0].position.Y];
                left = currentNode.impassable;    
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, 1);
            }

            // Right
            if (viewable[0].position.Y >= 0 && viewable[0].position.X + 1 < masterGrid.Count && !masterGrid[(int)viewable[0].position.X + 1][(int)viewable[0].position.Y].impassable)
            {
                currentNode = masterGrid[(int)viewable[0].position.X + 1][(int)viewable[0].position.Y];
                right = currentNode.impassable;
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, 1);
            }

            if(allowDiagnals)
            {
                // Up and Right, whats different on these is that we go diagnal and we check if both up and right need to be open (passable) "illustate" so if up and right both blocked we cant go through, same for thr rest 
                if (viewable[0].position.X >= 0 && viewable[0].position.X + 1 < masterGrid.Count && viewable[0].position.Y > 0 && viewable[0].position.Y < masterGrid[0].Count && !masterGrid[(int)viewable[0].position.X + 1][(int)viewable[0].position.Y - 1].impassable && (!up || !right))
                {
                    currentNode = masterGrid[(int)viewable[0].position.X + 1][(int)viewable[0].position.Y - 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, (float)Math.Sqrt(2));
                }

                //Down and Right
                if (viewable[0].position.X >= 0 && viewable[0].position.X + 1 < masterGrid.Count && viewable[0].position.Y >= 0 && viewable[0].position.Y + 1 < masterGrid[0].Count && !masterGrid[(int)viewable[0].position.X + 1][(int)viewable[0].position.Y + 1].impassable && (!down || !right))
                {
                    currentNode = masterGrid[(int)viewable[0].position.X + 1][(int)viewable[0].position.Y + 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, (float)Math.Sqrt(2));
                }

                //Down and Left
                if (viewable[0].position.X > 0 && viewable[0].position.X < masterGrid.Count && viewable[0].position.Y >= 0 && viewable[0].position.Y + 1 < masterGrid[0].Count && !masterGrid[(int)viewable[0].position.X - 1][(int)viewable[0].position.Y + 1].impassable && (!down || !left))
                {
                    currentNode = masterGrid[(int)viewable[0].position.X - 1][(int)viewable[0].position.Y + 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, (float)Math.Sqrt(2));
                }

                // Up and Left
                if (viewable[0].position.X > 0 && viewable[0].position.X < masterGrid.Count && viewable[0].position.Y > 0 && viewable[0].position.Y < masterGrid[0].Count && !masterGrid[(int)viewable[0].position.X - 1][(int)viewable[0].position.Y - 1].impassable && (!up || !left))
                {
                    currentNode = masterGrid[(int)viewable[0].position.X - 1][(int)viewable[0].position.Y - 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].position.X, viewable[0].position.Y), viewable[0].currentDistance, end, (float)Math.Sqrt(2));
                }
            }

            // Now we set the node to used, add it to the used list and remove it from the viewable list (move it from the viewable to the used)
            viewable[0].hasBeenUsed = true;
            used.Add(viewable[0]);
            viewable.RemoveAt(0);

        }

        // Setting the A* node
        public void SetAStarNode(List<GridLocation> viewable, List<GridLocation> used, GridLocation nextNode, Vector2 nextParent, float distance, Vector2 target, float distanceMultiply)
        {
            // Setting the fscore to distance   
            float fscore = distance;
            float addedDistance = (nextNode.cost * distanceMultiply);


            // Add item If we havent found it yet and havent been used
            if(!nextNode.isViewable && !nextNode.hasBeenUsed)
            {
                nextNode.SetNode(nextParent, fscore, distance + addedDistance);
                nextNode.isViewable = true;

                // We insert it
                SetAStarNodeInsert(viewable, nextNode);
            }

            // Node is in viewable, so we need to check if fscore needs revised
            else if(nextNode.isViewable)
            {
                if(fscore < nextNode.fscore)
                {
                    // We reset that node, we get all of the new things
                    nextNode.SetNode(nextParent, fscore, distance + addedDistance);
                }
            }
        }

        // The sort, insetrts it if it find it in the right spot else adds it right in the end
        public virtual void SetAStarNodeInsert(List<GridLocation> list, GridLocation newNode) // At first i used this one n^2.. Change no NlogN sorting algorithem like merge or quick
        {
            bool added = false;
            for(int i = 0; i<list.Count; i++)
            {
                if(list[i].fscore > newNode.fscore)
                {
                    list.Insert(Math.Max(1, i), newNode);
                    added = true;
                    break;
                }
            }

            if(!added)
            {
                list.Add(newNode);
            }

        }
        #endregion

        public virtual void DrawGrid(Vector2 offset)
        {
            if(showGrid)
            {
                // Getting the top left and bottom right pixels so we will only draw in these bounds and not all of the pixels all the time
                Vector2 topLeft = new Vector2(0, 0);
                Vector2 bottomRight = gridDimansions;

                for(int i = (int)topLeft.X; i<=bottomRight.X && i<slots.Count; i++)
                {
                    for(int j = (int)topLeft.Y; j<=bottomRight.Y && j<slots[0].Count; j++)
                    {
                        // If this is the slot we are hovering, shade it with red else leave it white
                        if (currentHoverSlot.X == i && currentHoverSlot.Y == j)
                        {
                            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.Red.ToVector4());
                            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
                        }
                        else if(slots[i][j].filled)
                        {
                            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.DarkGray.ToVector4());
                            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
                        }
                        else
                        {
                            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
                        }


                        // Drawing the slot
                        gridImage.Draw(offset + physicalStartPosition + new Vector2(i * slotDimensions.X, j * slotDimensions.Y));
                    }
                }
            }

            for (int i = 0; i<gridItems.Count; i++)
            {
                gridItems[i].Draw(offset);
            }
        }

    }
}
