# FDV_06_Sprites

1. Para mover al personaje, se adaptaron códigos creados previamente. Al ser un Sprite 2D, ejecutamos el método de _Flip_ al Sprite cuando cambia el Input, juntos cambia la animación entre De _Idle_ y _Walking_.
Para realizar el salto, aplicamos una fuerza al RigidBody, para evitar que el salto quede indefinido, creamos una variable `IsOnFloor` que cambia de verdadero a falso si el personaje no está en contacto con el GameObject con la etiqueta _Floor_.

    - mover el personaje:

    ```C#
    HorInput = Input.GetAxis("Horizontal") * SpeedMov;
    transform.Translate(HorInput, 0, 0);
    ```

    - Cambiar la dirección y la animación de los sprites:

    ```C#
    if (ZombieSprite != null)
            {
                if (HorInput < 0)
                {
                    animator.SetBool("IsWalking", true);
                    ZombieSprite.flipX = true;
                }
                else if (HorInput > 0)
                {
                    animator.SetBool("IsWalking", true);
                    ZombieSprite.flipX = false;
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                }
                transform.Translate(HorInput, 0, 0);
            }
    ```

    - Movimiento de Salto:

    ![Sprites - Movimento sem Salto](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/Zombie_walk.gif)

    - Movimento con Salto:

        ```C#
        void Jump()
            {
                Zombie_Rigidbody.AddForce(transform.up * Impulse);
                IsOnFloor = false;
            }
        ```

        ![Sprites - Salto/Jump](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/Zombie_walk_jump.gif)

2. Para cambiar la animación del zombie se utilizó el siguiente código:

    ```C#
    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "Enemy")
                animator.SetBool("IsDead", true);

            if (other.gameObject.name == "Goblin")
            {
                OtherAnimator = other.gameObject.GetComponent<Animator>();
                OtherAnimator.SetBool("CanAttackZombie", true);
            }

            if (other.gameObject.CompareTag("Floor"))
                IsOnFloor = true;
        }
    ```

    ![Sprites - Parmetros da animaçao](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/Animator.png)
    

    - zombie con animación muerta:

    ![Sprites - animaçao de morto](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/Zombie_dead.gif)

    - Duende con animación de ataque:

    ![Sprites - animaçao de ataque](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/Goblin_Attack.gif)

3. Se introdujo una cápsula que contenía un componente RigidBody en el escenario y se agregó un componente _SpringJoint2D_ al Goblin, lo que hizo que el Goblin se moviera como un __péndulo__..

    ![Sprites - SpringJoint2D](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/Goblin_Joint.gif)

4. Pruebas de física 2D
    1. Los objetos permanecen inmóviles, no podemos moverlos. Las leyes de la física no se aplican a ellos.

        ![Sprites - Fisica 1](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/fisica_1.gif)

    2. Las Zombie está sujeta a las leyes de la física, por lo que se ven afectadas por una gravedad diferente al Goblin.

         ![Sprites - Fisica 2](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/fisica_2.gif)

    3. Todos los Sprites tienen física, por lo que la gravedad los atrae hacia el suelo. Cuando chocan con la cápsula, muévela.

         ![Sprites - Fisica 3](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/fisica_3.gif)

    4. El zombie, al tener una masa 10 veces mayor que el Goblin, al chocar con el Goblin lo desplaza fácilmente.

         ![Sprites - Fisica 4](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/fisica_4.gif)

    5. El zombie se solapa con el goblin, pero no pasa nada porque no tenemos una función programada para tal evento.

         ![Sprites - Fisica 5](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/fisica_5.gif)

    6. Los objetos al tener física y el Goblin al etener como activo el IsTrigger cae indefinidamente.

         ![Sprites - Fisica 6](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/fisica_6.gif)

    7. El Goblin, al no estar sujeto a las leyes de la física, no se mueve a pesar de la colisión con el zombie.

         ![Sprites - Fisica 7](https://github.com/almadacv/FDV_06_Sprites/blob/main/Gif/fisica_7.gif)
