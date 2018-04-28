using System;
using UnityEngine;

public class CubeLogic
{
    enum Color { Blue, Green, White, Yellow, Red, Brown };

    Color[,] face = new Color[2, 2];
    Color[,] back = new Color[2, 2];
    Color[] topOut = new Color[2];
    Color[] bottomOut = new Color[2];
    Color[] leftOut = new Color[2];
    Color[] rightOut = new Color[2];

    Color[] topIn = new Color[2];
    Color[] bottomIn = new Color[2];
    Color[] leftIn = new Color[2];
    Color[] rightIn = new Color[2];

    int count = 0;

    public CubeLogic()
    {
        InitCubeLogic();
    }

    public void InitCubeLogic()
    {
        for (int i = 0; i < 2; i++)
        {
            topOut[i] = Color.White;
            topIn[i] = Color.Yellow;

            bottomOut[i] = Color.Yellow;
            bottomIn[i] = Color.White;

            leftOut[i] = Color.Red;
            leftIn[i] = Color.Brown;

            rightOut[i] = Color.Brown;
            rightIn[i] = Color.Red;

            for (int j = 0; j < 2; j++)
            {
                face[i, j] = Color.Blue;
                back[i, j] = Color.Green;
            }
        }
    }

    public bool CheckSolve()
    {
        bool solve = true;
        solve &= topOut.CheckAllEquals();
        solve &= bottomOut.CheckAllEquals();
        solve &= leftOut.CheckAllEquals();
        solve &= rightOut.CheckAllEquals();
        solve &= face.CheckAllEquals();
        solve &= back.CheckAllEquals();
        Debug.Log(solve);
        return solve;
    }

    public void CubeLogicDebug()
    {
        Debug.Log("===========================================");
        Debug.Log("FACE");
        face.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("BACK");
        back.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("TOP OUT");
        topOut.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("TOP IN");
        topIn.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("BOTTOM OUT");
        bottomOut.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("BOTTOM IN");
        bottomIn.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("LEFT OUT");
        leftOut.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("LEFT IN");
        leftIn.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("RIGHT OUT");
        rightOut.DebugToString();
        Debug.Log("===========================================");
        Debug.Log("RIGHT IN");
        rightIn.DebugToString();
        Debug.Log("===========================================");
        Debug.Log(count);
    }

    public void CheckRotation(string axisString, bool rotation = false, bool leftSwipe = false)
    {
        Utils.Axis axis = (Utils.Axis)Enum.Parse(typeof(Utils.Axis), axisString);

        switch (axis)
        {
            case Utils.Axis.LeftAxis:
                count++;
                if (rotation)
                {
                    if (leftSwipe)
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[0, 0] = leftIn[0];
                        face[1, 0] = leftIn[1];

                        Color[] leftOutBackup = leftOut.Clone() as Color[];
                        leftOut[0] = faceBackup[0, 0];
                        leftOut[1] = faceBackup[1, 0];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[0, 0] = leftOutBackup[0];
                        back[1, 0] = leftOutBackup[1];

                        leftIn[0] = backBackup[0, 0];
                        leftIn[1] = backBackup[1, 0];
                    }
                    else
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[0, 0] = leftOut[0];
                        face[1, 0] = leftOut[1];

                        Color[] leftInBackup = leftIn.Clone() as Color[];
                        leftIn[0] = faceBackup[0, 0];
                        leftIn[1] = faceBackup[1, 0];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[0, 0] = leftInBackup[0];
                        back[1, 0] = leftInBackup[1];

                        leftOut[0] = backBackup[0, 0];
                        leftOut[1] = backBackup[1, 0];
                    }
                }
                else
                {
                    Color[,] faceBackup = face.Clone() as Color[,];
                    face[0, 0] = back[1, 0];
                    face[1, 0] = back[0, 0];
                    back[0, 0] = faceBackup[1, 0];
                    back[1, 0] = faceBackup[0, 0];

                    Color[] topOutBackup = topOut.Clone() as Color[];
                    topOut[0] = bottomOut[0];
                    bottomOut[0] = topOutBackup[0];

                    Color[] bottomInBackup = bottomIn.Clone() as Color[];
                    bottomIn[0] = topIn[0];
                    topIn[0] = bottomInBackup[0];

                }
                break;

            case Utils.Axis.RightAxis:
                count++;
                if (rotation)
                {
                    if (leftSwipe)
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[0, 1] = rightOut[0];
                        face[1, 1] = rightOut[1];

                        Color[] rightInBackup = rightIn.Clone() as Color[];
                        rightIn[0] = faceBackup[0, 1];
                        rightIn[1] = faceBackup[1, 1];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[0, 1] = rightInBackup[0];
                        back[1, 1] = rightInBackup[1];

                        rightOut[0] = backBackup[0, 1];
                        rightOut[1] = backBackup[1, 1];

                    }
                    else
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[0, 1] = rightIn[0];
                        face[1, 1] = rightIn[1];

                        Color[] rightOutBackup = rightOut.Clone() as Color[];
                        rightOut[0] = faceBackup[0, 1];
                        rightOut[1] = faceBackup[1, 1];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[0, 1] = rightOutBackup[0];
                        back[1, 1] = rightOutBackup[1];

                        rightIn[0] = backBackup[0, 1];
                        rightIn[1] = backBackup[1, 1];
                    }
                }
                else
                {
                    Color[,] faceBackup = face.Clone() as Color[,];
                    face[0, 1] = back[1, 1];
                    face[1, 1] = back[0, 1];
                    back[0, 1] = faceBackup[1, 1];
                    back[1, 1] = faceBackup[0, 1];

                    Color[] topOutBackup = topOut.Clone() as Color[];
                    topOut[1] = bottomOut[1];
                    bottomOut[1] = topOutBackup[1];

                    Color[] bottomInBackup = bottomIn.Clone() as Color[];
                    bottomIn[1] = topIn[1];
                    topIn[1] = bottomInBackup[1];

                }
                break;
            case Utils.Axis.TopAxis:
                count++;
                if (rotation)
                {
                    if (leftSwipe)
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[0, 0] = topOut[0];
                        face[0, 1] = topOut[1];

                        Color[] topInBackup = topIn.Clone() as Color[];
                        topIn[0] = faceBackup[0, 0];
                        topIn[1] = faceBackup[0, 1];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[0, 0] = topInBackup[0];
                        back[0, 1] = topInBackup[1];

                        topOut[0] = backBackup[0, 0];
                        topOut[1] = backBackup[0, 1];
                    }
                    else
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[0, 0] = topIn[0];
                        face[0, 1] = topIn[1];

                        Color[] topOutBackup = topOut.Clone() as Color[];
                        topOut[0] = faceBackup[0, 0];
                        topOut[1] = faceBackup[0, 1];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[0, 0] = topOutBackup[0];
                        back[0, 1] = topOutBackup[1];

                        topIn[0] = backBackup[0, 0];
                        topIn[1] = backBackup[0, 1];
                    }
                }
                else
                {
                    Color[,] faceBackup = face.Clone() as Color[,];
                    face[0, 0] = back[0, 1];
                    face[0, 1] = back[0, 0];
                    back[0, 0] = faceBackup[0, 1];
                    back[0, 1] = faceBackup[0, 0];

                    Color[] leftOutBackup = leftOut.Clone() as Color[];
                    leftOut[0] = rightOut[0];
                    rightOut[0] = leftOutBackup[0];

                    Color[] leftInBackup = leftIn.Clone() as Color[];
                    leftIn[0] = rightIn[0];
                    rightIn[0] = leftInBackup[0];
                }
                break;

            case Utils.Axis.BottomAxis:
                count++;
                if (rotation)
                {
                    if (leftSwipe)
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[1, 0] = bottomIn[0];
                        face[1, 1] = bottomIn[1];

                        Color[] bottomOutBackup = bottomOut.Clone() as Color[];
                        bottomOut[0] = faceBackup[1, 0];
                        bottomOut[1] = faceBackup[1, 1];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[1, 0] = bottomOutBackup[0];
                        back[1, 1] = bottomOutBackup[1];

                        bottomIn[0] = backBackup[1, 0];
                        bottomIn[1] = backBackup[1, 1];
                    }
                    else
                    {
                        Color[,] faceBackup = face.Clone() as Color[,];
                        face[1, 0] = bottomOut[0];
                        face[1, 1] = bottomOut[1];

                        Color[] bottomInBackup = bottomIn.Clone() as Color[];
                        bottomIn[0] = faceBackup[1, 0];
                        bottomIn[1] = faceBackup[1, 1];

                        Color[,] backBackup = back.Clone() as Color[,];
                        back[1, 0] = bottomInBackup[0];
                        back[1, 1] = bottomInBackup[1];

                        bottomOut[0] = backBackup[1, 0];
                        bottomOut[1] = backBackup[1, 1];
                    }
                }
                else
                {
                    Color[,] faceBackup = face.Clone() as Color[,];
                    face[1, 0] = back[1, 1];
                    face[1, 1] = back[1, 0];
                    back[1, 0] = faceBackup[1, 1];
                    back[1, 1] = faceBackup[1, 0];

                    Color[] leftOutBackup = leftOut.Clone() as Color[];
                    leftOut[1] = rightOut[1];
                    rightOut[1] = leftOutBackup[1];

                    Color[] leftInBackup = leftIn.Clone() as Color[];
                    leftIn[1] = rightIn[1];
                    rightIn[1] = leftInBackup[1];
                }
                break;
        }
    }
}