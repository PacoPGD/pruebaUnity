﻿using UnityEngine;
using System.Collections;

public class Jugador : Actor {

	public int nJugador;


	Texture2D [] corazon;
	private int corazones = 3;
	private int vidaCorazon = 20;

	Texture2D rupia;
	private static int rupias = 0;
	private static int rupiasMax = 99;

	Texture2D botones;

	public void Start () {
		base.Start ();
		vidaMax = corazones*vidaCorazon;
		vida = vidaMax;
		cargaImagenes ();
	}
	
	public void Update() {
		base.Update ();
	}
	
	public override float[] movimiento (){
		float [] move = new float[2];
		float speed = 6;
		
		
		if (nJugador == 1) {
			move[0] = Input.GetAxis ("Horizontal1");
			move[1] = Input.GetAxis ("Vertical1");
		} 
		else {
			move[0] = Input.GetAxis ("Horizontal2");
			move[1] = Input.GetAxis ("Vertical2");
		}
		
		if (move [0] != 0 && move [1] != 0) 
			speed = Mathf.Sqrt (Mathf.Pow (speed, 2) * 2) / 2;
			
		//if (Mathf.Abs (move [0]) > 0.4 || Mathf.Abs (move [1]) > 0.4)
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (move [0] * speed, move [1] * speed);
		//else
		//	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

		return move;
	}


	/*FUNCIONES DE PINTADO*/
	void OnGUI(){
		pintaCorazones();
		pintaRupias();
		pintaBotones();
	}

	/*Funcion que pinta todos los corazones los cuales representan la vida del jugador*/
	void pintaCorazones(){
		int i;
		int pintaCorazones; //Cuantos corazones completos pintamos
		float pintaPorcion; //Vida que hay en el resto para saber que porcion dibujar

		/*Calculo los corazones que hay que pintar dividiendo la vida entre la vida que tiene un solo corazon*/
		pintaCorazones = vida / vidaCorazon;

		/*El resto de la division se usa para representar un corazon no completo*/
		pintaPorcion = vida - (vidaCorazon * pintaCorazones);


		/*Pinto corazones vacios que representan la vida maxima*/
		for(i=0;i<corazones;i++){
			pintaUnCorazon (i,corazon[4]);
		}

		/*Pinto corazones llenos*/
		for(i=0;i<pintaCorazones;i++){
			pintaUnCorazon (i,corazon[0]);
		}

		/*Pinto un corazon partido en relacion al valor de vida restante*/
		if (pintaPorcion > vidaCorazon * (0.75)) {
			pintaUnCorazon (i,corazon[1]);
		} else if (pintaPorcion > vidaCorazon * (0.50)) {
			pintaUnCorazon (i,corazon[2]);
		} else if (pintaPorcion > 0) {
			pintaUnCorazon (i,corazon[3]);
		}

	}

	/*Funcion que pinta un corazon, recibe la posicion del corazon en i y la textura que deseamos pintar*/
	void pintaUnCorazon(int i, Texture2D corazon){
		int desplazamientoJugador; //Desplazamiento extra para situar los corazones de cada jugador en un lugar
		if (nJugador == 1)
			desplazamientoJugador = 30;
		else
			desplazamientoJugador = 780;

		if(i>=10)
			GUI.Label (new Rect (desplazamientoJugador+(i-10)*25,35,30,30), corazon);
		else
			GUI.Label (new Rect (desplazamientoJugador+i*25,10,30,30), corazon);

	}

	void pintaRupias(){
		string textoRupia;
		textoRupia = "" + rupias;

		GUIStyle estilo = new GUIStyle();

		estilo.fontSize = 30;

		if (nJugador == 1) {
			GUI.Label (new Rect (75, 550, 30, 30), rupia);
			GUI.Label (new Rect (40, 550, 30, 30), textoRupia,estilo);
		}
	}

	void pintaBotones(){
		if(nJugador==1)
			GUI.Label (new Rect (300, 25, 75, 75), botones);
		else
			GUI.Label (new Rect (700, 25, 75, 75), botones);
	}

	/*FIN DE FUNCIONES DE PINTADO*/

	/*Funcion que carga todas las imagenes que se necesitaran en la clase de jugador, como pueden ser los corazones
	 * para la vida o el contador de rupias*/
	public void cargaImagenes(){
		corazon = new Texture2D[5];
		corazon[0] = (Texture2D)Resources.Load("corazon/corazonlleno");
		corazon[1] = (Texture2D)Resources.Load("corazon/corazon34");
		corazon[2] = (Texture2D)Resources.Load("corazon/corazonmedio");
		corazon[3] = (Texture2D)Resources.Load("corazon/corazoncuarto");
		corazon[4] = (Texture2D)Resources.Load("corazon/corazonvacio");
		
		rupia = (Texture2D)Resources.Load("rupia");

		botones = (Texture2D)Resources.Load("botones");
	}


}
