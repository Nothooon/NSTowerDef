## Arborescence du projet



Dans notre projet, nous travaillons presque exclusivement dans /Assets/. En effet les autres dossiers sont des fichiers de base d’unity tels que les paramètres, les librairies (qui ne sont donc pas à inclure dans le git). 

Nous allons donc détailler l’arborescence de /Assets/.


![1](https://user-images.githubusercontent.com/83947562/123275896-0166d000-d505-11eb-81fd-aecf2514d102.png)
![21](https://user-images.githubusercontent.com/83947562/123276669-ac778980-d505-11eb-8071-a66a47041d43.png)

Les éléments principaux de notre projet sont des prefabs, c’est-à-dire des objets. Un prefab est une hiérarchie de gameObject. Un gameObject est une entité qui est composée de différents scripts, qui dans unity sont appelés des composants. 


## Exemple d’objet :
Tourelle basique : fichier “turret_base.prefab”


Notre fichier prefab est composé de deux gameObject, un parent et son enfant.

![unnamed (1)](https://user-images.githubusercontent.com/83947562/123277276-40e1ec00-d506-11eb-847d-3c1acccbea1e.png)

### Composants du premier gameObject "turret_base"  

![unnamed](https://user-images.githubusercontent.com/83947562/123277346-522af880-d506-11eb-8a7b-d16033fc29f4.png)

 - Transform : composant de base permettant que ce gameObject ait une position, une rotation, et une échelle.

 - SpriteRenderer : composant permettant que ce gameObject ait un rendu, c'est-à-dire une image (importé dans Assets/import/Images), une couleur, un matériel.


### Composants du deuxième gameObject “turret_canon” 
![pasted image 0](https://user-images.githubusercontent.com/83947562/123277357-548d5280-d506-11eb-90d4-3152dcb7273f.png)

 - Transform

 - Sprite Renderer

 - TurretSelection : un script qui gère la recherche d’une cible ennemie, de tourner le canon et de tirer. On remarque que l’on peut fixer les variables publiques du script dans le prefab (ici price = 100; range = 3; fireRate = 1; projectile Prefab = projectile; projectileSpeed = 10000).

 - BoxCollider2D : composant créant une hitbox à notre objet ( masque de collision ).
 
 - TurretUpgradeSell : un script qui gère l’amélioration de la tourelle, et la revente.
On voit donc que ce qui définit un gameObject sont les composants ou scripts attachés et les paramètres passés aux scripts.  Ici les paramètres importants sont le prix de la tourelle, sa portée, sa cadence de tir, la vitesse de son projectile et surtout son type de projectile. 
Ce dernier est lui aussi un prefab, avec un seul gameObject qui comporte une collision et un script définissant le comportement du projectile à l’impact. Le projectile du canon par exemple applique des dégâts en zone, tandis que celui de la baliste peut toucher plusieurs ennemis, celui du canonGlue ralentit sa cible. Ces différents scripts peuvent également avoir des paramètres à faire varier pour créer ou équilibrer le jeu,  comme la puissance, la portée de l’explosion,le nombre d'ennemis touchables.

## Exemple : création d’une nouvelle tourelle

Pour ajouter une tourelle à notre projet, il faudrait donc créer un nouveau prefab inspiré de ceux existant, avec un gameObject pour la base, et un gameObject pour le canon, ce qui permet de faire tourner uniquement l’un des deux gameObject. Par la suite, il faut gérer tous les paramètres, si besoin créer un projectile adapté.
 Il faut ensuite le rajouter au shop en créant un nouveau bouton et l’ajouter au paramètre du script turretAdding de turretBuilder (objet gérant l’ajout des tourelles).
