using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y; 
    [SerializeField] int z;
    [SerializeField] int ite;
    [SerializeField] int survival;
    [SerializeField] int birth;
    [SerializeField] GameObject blankCell;
    [SerializeField] GameObject fullCell;
    [SerializeField] Button generateButton;
    [SerializeField] TMP_InputField sizeX_IF;
    [SerializeField] TMP_InputField sizeY_IF;
    [SerializeField] TMP_InputField sizeZ_IF;
    [SerializeField] TMP_InputField survival_IF;
    [SerializeField] TMP_InputField birth_IF;
    [SerializeField] Toggle mvnTooggle;
    List<GameObject> currentCells = new List<GameObject>();
    int[, ,] cells;
    float timer;
    bool generate;
    bool moore_vonNewman;
    bool timerReached;
    int genToDie = 3;

     void Start()
    {
        generate = false;
        timerReached = false;
        timer = 0;
    } 

    void Update()
    {
        if(timerReached == false){
            timer += Time.deltaTime;
        }
         //Condicion para que se cree el automata celular.
         if(generate == true && timerReached == false && timer > 1) {
            timer = 0;
            int[, ,] nextCells = new int [x, y, z];
            /*Se itera sobre todas las celdas para checar sus vecindarios(Dependiendo
            de el estado de la celda se compara con survival o birth).*/
            for (int k = 0; k < z; k++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int i = 0; i < x; i++)
                    {
                        //Debug.Log(i + ", " + j + ", " + k + " =" + cells[i, j, k]);
                        int num = numOfFilledCells(i, j, k);
                        /*Si la celda tiene un estado de 0, se checara el numero de 1s
                         q tiene alrededor para saber si puede revivir en la sig generacion.*/
                       if(IsFilled(i, j, k) == 0 ){
                        if(num >= birth){
                            Debug.Log("birth");
                            nextCells[i, j, k] = genToDie;
                        }
                        /* Si la celda tiene un estado de 1, se checara el numero de 1s 
                            que tiene alrededor para saber si sobrevivi o muere en la sig generacion.*/
                        } else {
                            if(num >= survival) {
                                //nextCells[i, j, k] = 3;
                            } else {
                                Debug.Log("dead");
                                if (nextCells[i, j, k] >= 1) {
                                    nextCells[i, j, k] = nextCells[i, j, k] - 1;
                                }
                            }
                        }
                    }

                }
            }
            //Actualiza las celdas actuales con la sig. generacion
            cells = nextCells;
            //Todos los gameObjects que teniamos, se borran y se ,limpia la lista.
            foreach (GameObject cell in currentCells)
            {
                Destroy(cell);
            }
            currentCells.Clear();
            //Se itera sobre todas las generaciones para saber si ponerle blanck o full.
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < z; k++)
                    {
                        if(IsFilled(i, j, k) == 0){
                            currentCells.Add(Instantiate(blankCell, new Vector3(i, j, k), Quaternion.identity));
                        } else {
                            currentCells.Add(Instantiate(fullCell, new Vector3(i, j, k), Quaternion.identity));
                        }
                    }

                }
            }
        }  
    } 
     
   //Se crea una grid random de 1 y 0.
    void CreateCells(){
        cells = new int [x, y, z];
        int [] randomNumber = {0, genToDie};
        for (int j = 0; j < x; j++)
        {
            for (int i = 0; i < y; i++)
            {
                for (int k = 0; k < z; k++) {
                    cells[i, j, k] = randomNumber[Random.Range(0, 2)];
                    if(cells[i, j, k] == 0) {
                        currentCells.Add(Instantiate(blankCell, new Vector3(i, j, k), Quaternion.identity));
                    } else {
                        currentCells.Add(Instantiate(fullCell, new Vector3(i, j, k), Quaternion.identity));
                    } 
                } 
            }
        } 
    }

    //Te regresa el numero de 1s que hay en i dependiendo del vecindario que seleccione el usuario.
     int numOfFilledCells(int _x, int _y, int _z){
        int num = 0;
        if (moore_vonNewman == true)
        {
            //Moore 
            for (int i = -1; i <= 1; i++)
            {
            for(int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
            {
               if(IsFilled(_x + i, _y + j, _z + k) != 0){
                    num++;
                }
            }
            } 
            }
        }
        return num;
    }

    //Toma los valores de la UI y los guarda en variables.
    public void GenerateCA(){
        x = int.Parse(sizeX_IF.text);
        y = int.Parse(sizeY_IF.text);
        z = int.Parse(sizeZ_IF.text);
        survival = int.Parse(survival_IF.text);
        birth = int.Parse(birth_IF.text);
        moore_vonNewman = mvnTooggle.isOn;
        generate = true;
        cells = new int[x, y, z];
        CreateCells();
    }

    //Devuelve si una celda esta llena o no.
    public int IsFilled(int _x, int _y, int _z){
        //Se checa si es una esquina o borde para regresar 0.
        if (_x < 0 || _x >= x || _y < 0 || _y >= y || _z < 0 || _z >= z) {
        return 0;
     } else { 
        return cells[_x, _y, _z];
     }
    }
}