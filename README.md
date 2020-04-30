# Compilador

## Léxico del lenguaje aceptado por el compilador 
#
1. ### IDENTIFICADOR:
* No comienza por un número.
* No puede contener caracteres especiales a excepción de los caracteres “_” y “$”.
* Contiene letras mayúsculas y minúsculas sin acentuación.

| IDENTIFICADOR   |  CATEGORIA  |
|----------|:-------------:|
| casa |     Identificador |
| $identificador|    Identificador |  
| _mi$variable | Identificador | 
| _123_Variable$ | Identificador |  
| 123_variables | Error |  
| ¿variables? | Error |  
| variable# | Error |   

#
2. ### NÚMEROS ENTEROS Y DECIMALES
* El separador decimal es ( , )

| NUMERO  |  CATEGORIA  |
|----------|:-------------:|
| 25 |     Número entero |
| 26,0|    Número decimal |  
| 0,1 | Número decimal| 
| 145, | Error |  
| ,5 | Error |  
| 4.8,3 | Error |  

#

3. ### OPERADORES MATEMÁTICOS

| OPERADOR   |  CATEGORIA  |
|----------|:-------------:|
| + |     Suma|
| - |    Resta |  
| * | Multiplicacion| 
| / | División |  
| % | Módulo |

#

4. ### ASIGNACION
| ASIGNACIÓN   |  CATEGORIA  |
|----------|:-------------:|
| :=  |     Asignación|

#

5. ### OPERADORES DE COMPARACIÓN

| OPERADOR   |  CATEGORIA  |
|----------|:-------------:|
| >  |     Mayor que|
| <  |     Menor que|
| =   |     Igual que|
| >=  |     Mayor o igual que|
| <=  |     Menor o igual que|
| <>  |     Diferente que|
| !=  |     Diferente que|

#

6. ### OPERADORES DE AGRUPACIÓN
| OPERADOR   |  CATEGORIA  |
|----------|:-------------:|
| (  |     Paréntesis abre|
| )  |     Paréntesis cierra|
 
 #

 7. ### ANEXOS
 * Elimina espacios en blanco
 * El salto de linea se reconoce como @FL@
 * El fin de archivo de reconoce como @EOF@
 * Permite comentarios de una sola linea // Ejemplo
 * Permite comentarios multilínea /* Ejemplo */
