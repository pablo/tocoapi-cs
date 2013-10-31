�Quer�s usar el API del Toco de manera realmente sencilla?
 ------ ---- -- --- --- ---- -- ------ --------- --------
 
Esta es tu mejor opci�n: El API del Toco Wrappead�simo en C#

Author: Pablo Santa (http://pablo.tocorre.com)
------ 
 
Version: 0.0.2 beta
-------
 
Requerimientos:
--------------
  * .NET 2.0
  * Visual Studio 2003/2005/2008 (prob� solo con 2005 y 2008, pero deber�a funcionar sin problemas con 2003)
  * Cualquier m�quina Windows con el Framework .NET. Si alguien lo compila y usa con MONO, le agradecer� que me lo comente. Deber�a funcionar.
  
El API es super sencillo y limpio. No requiere absolutamente ning�n conocimiento de XML-RPM, HTTP, SOCKETS ni NADA. Solamente C#. Punto.

Archivos:
--------
-> XmlRpc.cs: implementaci�n (completa?) de XML-RPC. Escrita "from scratch" y sin ninguna librer�a externa. Gracias SMS. (namespace tocorre.XmlRpc)
-> TocoAPI.cs: wrapper del API del Toco (namespace tocorre.TocoAPI)
-> Pogram.cs: programita de prueba super sencillo

Uso:
---

Para usar el API del Toco hay que hacer cosas tan simples y claras como estas cuatro l�neas:

//
Toco t = new Toco;
t.Login("tu_email@de_tocorre.com", "tu_password");
t.SetMotd(0, "Este es mi MotD seteado con el Toco API carajo!");
t.ExecuteSystemMultiCall();
//

Y ya est�! Nada m�s.

Bugs conocidos:
---- ---------

Ninguno, que yo sepa. Pero cualquier observaci�n, bug, comentario, puteada o lo que quieran favor dirigir a pablo@roshka.com.py

Sugerencias a los desarrolladores que agarren esto:
----------- - --- --------------- --- ------- ----

Esto es Windows muchachos. Si hacen un lindo programita en C#, la gente del Toco va a usar!

Algunas cosas r�pidas que se me ocurren:

* El iTunes 2 MOTD (Message Of The Day) {gracias Jan'i)
* Una aplicacioncita que vaya al TrayIcon y que despliegue una ventanita tipo MSN cuando el usuario recibe un scrap/mensaje nuevo
* Una aplicaci�n esp�a para "espiar" perfiles de otros usuarios y estar atentos ante alguna nota nueva en el scrap de ese usuario

Notas:
-----
* Gracias Bill Gates por mostrarnos tantas veces el camino
* Valor Absoluto de |T| -> NO TEN�S VISI�OOOOOOOOOOOOON!
* Merezco lejos, haber sido creado por generaci�n espont�nea
* Gracias Emma por tanta alegr�a

ASC, ASC, ASC....




