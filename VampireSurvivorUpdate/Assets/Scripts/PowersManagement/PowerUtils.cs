using UnityEngine;

namespace PowersManagement
{
    public class PowerUtils : MonoBehaviour
    {
    
        // <summary>
        // Method to get a random enemy object (if mustBeOnViewport is true, the object returned is on the viewport)
        // </summary>
        // <param name="mustBeOnViewport">If true, the object returned is on the viewport</param>
        // <returns>Random enemy object</returns>
        public static GameObject GetRandomEnemyObject(bool mustBeOnViewport){
            // Get all the enemies in the scene
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // If there are enemies in the scene
            if (enemies.Length > 0)
            {
                // Get a random enemy
                int randomIndex = Random.Range(0, enemies.Length);
                GameObject randomEnemy = enemies[randomIndex];
                // If the enemy must be on the viewport
                if (mustBeOnViewport && IsEnemyOnViewport())
                {
                    // Get the viewport position of the random enemy
                    Vector3 viewportPosition = Camera.main.WorldToViewportPoint(randomEnemy.transform.position);
                    // If the viewport position is not on the viewport
                    if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
                    {
                        // Get a new random enemy
                        randomEnemy = GetRandomEnemyObject(true);
                    }
                }
                // Return the random enemy
                return randomEnemy;
            }
            // Return null
            return null;
        }
    
        // <summary>
        // Method to get a random enemy position (if mustBeOnViewport is true, the position returned is on the viewport)
        // </summary>
        // <param name="mustBeOnViewport">If true, the position returned is on the viewport</param>
        // <returns>Position of a random enemy</returns>
        public static Vector3 GetRandomEnemyPosition(bool mustBeOnViewport)
        {
            // Get Vectore3.zero as the default randomPosition value
            Vector3 randomPosition = Vector3.zero;
            // Get all enemies in the scene
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // If there are enemies in the scene
            if (enemies.Length > 0)
            {
                // Get a random enemy
                int randomIndex = Random.Range(0, enemies.Length);
                // Get the position of the random enemy
                randomPosition = enemies[randomIndex].transform.position;
                // If the position must be on the viewport
                if (mustBeOnViewport && IsEnemyOnViewport())
                {
                    // Get the viewport position of the random enemy
                    Vector3 viewportPosition = Camera.main.WorldToViewportPoint(randomPosition);
                    // If the viewport position is not on the viewport
                    if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
                    {
                        // Get a new random position
                        randomPosition = GetRandomEnemyPosition(true);
                    }
                }
            }
            // Return the random position
            return randomPosition;
        }
    
        // <summary>
        // Method to check if there is an enemy on the viewport
        // </summary>
        // <returns>True if there is an enemy on the viewport, false otherwise</returns>
        public static bool IsEnemyOnViewport()
        {
            // Get all enemies in the scene
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // If there are enemies in the scene
            if (enemies.Length > 0)
            {
                // For each enemy
                foreach (GameObject enemy in enemies)
                {
                    // Get the viewport position of the enemy
                    Vector3 viewportPosition = Camera.main.WorldToViewportPoint(enemy.transform.position);
                    // If the viewport position is on the viewport
                    if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                    {
                        // Return true
                        return true;
                    }
                }
            }
            // Return false
            return false;
        }

        // <summary>
        // Method to check if a position is on the viewport
        // </summary>
        // <param name="position">Position to check</param>
        // <returns>True if the position is on the viewport, false otherwise</returns>
        public static bool IsPositionOnViewport(Vector3 position)
        {
            // Get the viewport position of the position
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(position);
            // If the viewport position is on the viewport
            if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                // Return true
                return true;
            }
            // Return false
            return false;
        }

        // <summary>
        // Method to check if a projectile is on the viewport
        // </summary>
        // <param name="projectile">Projectile to check</param>
        // <returns>True if the projectile is on the viewport, false otherwise</returns>
        public static bool IsProjectileOnViewport(GameObject projectile)
        {
            // returns true if the projectile is on the viewport
            return IsPositionOnViewport(projectile.transform.position);
        }

        // <summary>
        // Method to get the nearest enemy position
        // </summary>
        // <param name="position">Position to check</param>
        // <returns>Position of the nearest enemy</returns>
        public static Vector3 GetNearestEnemyLocation(Vector3 position)
        {
            // Get all enemies in the scene
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // If there are enemies in the scene
            if (enemies.Length > 0)
            {
                // Get the first enemy as the nearest enemy
                GameObject nearestEnemy = enemies[0];
                // Get the distance between the position and the first enemy
                float nearestDistance = Vector3.Distance(position, nearestEnemy.transform.position);
                // For each enemy
                foreach (GameObject enemy in enemies)
                {
                    // Get the distance between the position and the enemy
                    float distance = Vector3.Distance(position, enemy.transform.position);
                    // If the distance is less than the nearest distance
                    if (distance < nearestDistance)
                    {
                        // Set the nearest enemy to the enemy
                        nearestEnemy = enemy;
                        // Set the nearest distance to the distance
                        nearestDistance = distance;
                    }
                }
                // Return the position of the nearest enemy
                return nearestEnemy.transform.position;
            }
            // Return Vector3.zero
            return Vector3.zero;
        }

        // <summary>
        // Method to get the position forward the player
        // </summary>
        // <param name="playerTransform">Transform of the player</param>
        // <param name="distance">Distance to the player</param>
        public static Vector3 GetPositionForwardPlayer(Transform playerTransform, float distance)
        {
            // Get the position of the player
            Vector3 playerPosition = playerTransform.position;
            // Get the forward vector of the player
            Vector3 playerForward = playerTransform.forward;
            // Get the position forward the player
            Vector3 positionForwardPlayer = playerPosition + playerForward * distance;
            // Return the position forward the player
            return positionForwardPlayer;
        }

        // <summary>
        // Method to get a random position on the viewport
        // </summary>
        // <returns>Random position on the viewport</returns>
        public static Vector3 GetRandomLocationOnViewport()
        {
            // Get a random position on the viewport
            Vector3 randomPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0));
            // Return the random position
            return randomPosition;
        }
    }
}
