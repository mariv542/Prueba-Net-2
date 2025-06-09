---
# Biblioteca Municipal
### Nombres: Cristobal Martinez, Hilary Varela 
---
## Web API RESTful en ASP.NET Core

La solución propuesta para la Biblioteca Municipal “Letras Libres” consiste en el desarrollo
de una Web API RESTful utilizando ASP.NET Core junto con Entity Framework Core como ORM,
lo que permitirá una gestión moderna, eficiente y escalable de libros, usuarios y préstamos.
Esta API proporcionará un conjunto de endpoints bien estructurados que facilitarán la
integración con aplicaciones móviles o web futuras, promoviendo así la digitalización completa
de los procesos bibliotecarios. La estructura de la solución contempla la creación de modelos 
representativos para libros, usuarios y préstamos, mapeados directamente a una base de datos
relacional mediante Entity Framework Core, lo cual asegura integridad, relaciones entre entidades
y un manejo eficiente de la información. La API ofrecerá operaciones CRUD completas para los 
libros y usuarios, además de funcionalidades específicas como el historial de préstamos por
usuario, registro de préstamos activos y devoluciones, lo cual permite llevar un control riguroso 
del inventario bibliográfico. Se implementarán reglas de negocio clave, como la restricción de 
eliminación de libros prestados, y se garantizará la seguridad y consistencia de los datos mediante 
validaciones y controles a nivel de API. Este enfoque moderno no solo optimiza el tiempo y 
recursos del personal bibliotecario, sino que también mejora la experiencia del usuario final 
al permitir un acceso rápido y centralizado a la información, sentando una base sólida para
desarrollos tecnológicos futuros en la institución.

---
## Diagrama De Relaciones UML
![image](https://github.com/user-attachments/assets/0ffc928b-72b2-4f22-a89d-3750854905e8)
---
## Validaciones 
Al momento de probar las rutas para verificar su funcionamiento, nos percatamos de que no habíamos
realizado las migraciones correspondientes a la base de datos, lo cual impedía que Entity Framework 
Core generara las tablas necesarias para el almacenamiento y manejo de la información. Esta omisión
evidenció la importancia de validar no solo la lógica de los controladores y endpoints, sino también
el correcto estado de la base de datos y su sincronización con los modelos definidos en el proyecto.
A partir de este punto, iniciamos el proceso de configuración y ejecución de las migraciones, 
asegurándonos de que el esquema de datos estuviera correctamente creado y listo para soportar las
operaciones que nuestra API RESTful requiere.

![image](https://github.com/user-attachments/assets/ceee78a4-2849-47c3-a359-3c582632b523)

Tras crear y aplicar las migraciones, insertamos datos de prueba en las tablas. Al ejecutar nuevamente
la aplicación, comprobamos que todas las rutas funcionan correctamente. El endpoint GET /api/libros 
muestra la lista de libros sin errores, lo que confirma que la base de datos está bien configurada y
la API responde como se espera.

![image](https://github.com/user-attachments/assets/3460f26d-b0bc-4f0b-b0ae-8769d18b2e3f)

En este caso hay un problema y no me deja buscar un libro por su id.

![image](https://github.com/user-attachments/assets/b0d7fc21-a6f2-4344-b532-fc9a29668fca)

Después de una investigación el problema que sale es por que Swagger UI se ejecuta en el navegador 
y el navegador aplica políticas de seguridad de CORS.  

CORS es una política de seguridad del navegador que impide que una pagina web como la que estamos 
usando haga solicitudes a un servidor si no está permitido explícitamente por ese servidor. 

En nuestro caso fallaba por que en el archivo programs.cs no incluía políticas CORS, por lo que el
navegador bloquea las respuestas, aunque el servidor contestara correctamente.

Por lo tanto, se le agregaron unas líneas al archivo de programs.cs para que usara y permitiera las 
políticas de CORS.

![image](https://github.com/user-attachments/assets/7e5a7506-2c09-419b-888f-425f7e4e5d86)

Después de esa modificación volvimos a lanzar la consulta y funciono con normalidad devolviendo el 
libro por su id.

![image](https://github.com/user-attachments/assets/2554acec-8151-4c9c-b5fc-aebf0de6f6e6)

Modificar libros funciono sin complicaciones.

![image](https://github.com/user-attachments/assets/3c4c827b-3618-49c8-8442-1e8d3e38bb0b)

![image](https://github.com/user-attachments/assets/32fe06db-4586-4050-a33b-80364e91cf67)

![image](https://github.com/user-attachments/assets/1da4b83e-9f5a-4bd6-95c9-a632572cccc5)

### DELETE /api/libros/{id} – Elimina un libro (solo si no está prestado)

![image](https://github.com/user-attachments/assets/5b050d99-516b-4362-acf7-4ddb23b03669)

![image](https://github.com/user-attachments/assets/d358f6f1-111c-45fe-8499-f5e9fa95cbf4)

Hay un detalle en esta ruta ya que si elimina un libro por la id con una función asíncrona,
pero hay un detalle según los requerimientos necesitamos que valide primero si el libro esta
prestado antes de eliminarlo, ya que, no debería eliminar un libro si esta eliminado además 
esto crea un conflicto que hace que falle.

![image](https://github.com/user-attachments/assets/4d15a258-3d8d-4fe4-a77d-4730c16194fb)

Después de simplemente colocar un if donde verifica si el libro esta prestado el cual no
permite eliminar un libro prestado y además le manda un mensaje de alerta.

![image](https://github.com/user-attachments/assets/3202c059-3541-41d4-8437-e18ede780df4)

Se verifico que en el gestor visual que realmente no borrara el registro y todavía está así
que todo correcto.

![image](https://github.com/user-attachments/assets/aba69bcc-fa77-45f6-9129-1ddb5dd7b131)

![image](https://github.com/user-attachments/assets/1c92fb04-0667-42af-a4f1-49ffbea79611)

Se agrego otro libro a la base de datos para asegurarnos que no tiene ningún libro guardado y 
otra vez tiro problemas con CORS.

Se le agrego esta línea en programs.cs y si ejecuto y realizo la respuesta esperada.

![image](https://github.com/user-attachments/assets/80d328e6-0422-4bf2-97ff-3df515ec2d4e)

![image](https://github.com/user-attachments/assets/11eae25c-746b-404a-8018-3d21c896601f)

![image](https://github.com/user-attachments/assets/1ce1fa3c-a7a9-4240-a95e-b9eaf33c2238)

### GET /api/usuarios/{id}/prestamos – Ver el historial de préstamos del usuario.

![image](https://github.com/user-attachments/assets/eaf4902c-9163-4551-b96c-ba3d30d1381d)

Al realizar la petición GET por id de usuario no hay complicaciones, pero en el resultado no
se muestra el historial de préstamos del usuario por lo tanto en el controlador solamente se 
modifican unas líneas.

![image](https://github.com/user-attachments/assets/08ab5d01-d242-48c4-ac04-9bff811ccc63)

![image](https://github.com/user-attachments/assets/5007ec38-3481-450f-a2ef-083d9f75e8f9)

Además, en programs.cs se le agregaron estas líneas para que este ReferenceHandler.Preserve 
le dice al serializador que maneje referencias cíclicas usando identificadores ($id, $ref),
lo cual evita que entre en bucles infinitos.

![image](https://github.com/user-attachments/assets/8a4d95a3-2b18-4a87-a1cf-3689c95720fb)

Y con esto entrega el resultado esperado.

![image](https://github.com/user-attachments/assets/626d81e7-2653-4f90-979b-edfb1844695a)

En conclusión, el proceso de validación de las rutas y funcionalidades de la API fue fundamental
para garantizar su correcto funcionamiento. Durante las pruebas, identificamos errores como la 
falta de migraciones iniciales y problemas al consultar libros por su ID. Estos inconvenientes 
fueron solucionados mediante la aplicación de migraciones, la carga de datos de prueba y la 
revisión detallada de los controladores y rutas. Gracias a estas correcciones, la API ahora responde 
adecuadamente a todas las solicitudes, demostrando una estructura sólida y preparada para futuras 
integraciones con aplicaciones móviles o web.









