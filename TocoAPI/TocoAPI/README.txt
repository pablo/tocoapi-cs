¿Querés usar el API del Toco de manera realmente sencilla?
 ------ ---- -- --- --- ---- -- ------ --------- --------
 
Esta es tu mejor opción: El API del Toco Wrappeadísimo en C#

Author: Pablo Santa (http://pablo.tocorre.com)
------ 
 
Version: 0.0.2 beta
-------
 
Requerimientos:
--------------
  * .NET 2.0
  * Visual Studio 2003/2005/2008 (probé solo con 2005 y 2008, pero debería funcionar sin problemas con 2003)
  * Cualquier máquina Windows con el Framework .NET. Si alguien lo compila y usa con MONO, le agradeceré que me lo comente. Debería funcionar.
  
El API es super sencillo y limpio. No requiere absolutamente ningún conocimiento de XML-RPM, HTTP, SOCKETS ni NADA. Solamente C#. Punto.

Archivos:
--------
-> XmlRpc.cs: implementación (completa?) de XML-RPC. Escrita "from scratch" y sin ninguna librería externa. Gracias SMS. (namespace tocorre.XmlRpc)
-> TocoAPI.cs: wrapper del API del Toco (namespace tocorre.TocoAPI)
-> Pogram.cs: programita de prueba super sencillo

Uso:
---

Para usar el API del Toco hay que hacer cosas tan simples y claras como estas cuatro líneas:

//
Toco t = new Toco;
t.Login("tu_email@de_tocorre.com", "tu_password");
t.SetMotd(0, "Este es mi MotD seteado con el Toco API carajo!");
t.ExecuteSystemMultiCall();
//

Y ya está! Nada más.

Bugs conocidos:
---- ---------

Ninguno, que yo sepa. Pero cualquier observación, bug, comentario, puteada o lo que quieran favor dirigir a pablo@roshka.com.py

Sugerencias a los desarrolladores que agarren esto:
----------- - --- --------------- --- ------- ----

Esto es Windows muchachos. Si hacen un lindo programita en C#, la gente del Toco va a usar!

Algunas cosas rápidas que se me ocurren:

* El iTunes 2 MOTD (Message Of The Day) {gracias Jan'i)
* Una aplicacioncita que vaya al TrayIcon y que despliegue una ventanita tipo MSN cuando el usuario recibe un scrap/mensaje nuevo
* Una aplicación espía para "espiar" perfiles de otros usuarios y estar atentos ante alguna nota nueva en el scrap de ese usuario

Notas:
-----
* Gracias Bill Gates por mostrarnos tantas veces el camino
* Valor Absoluto de |T| -> NO TENÉS VISIÓOOOOOOOOOOOOON!
* Merezco lejos, haber sido creado por generación espontánea
* Gracias Emma por tanta alegría

ASC, ASC, ASC....




