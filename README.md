# CursoCleanArchitecture
 Clean Architecture y Domain Driven Design en ASP.NET Core 



# crear network para curso
docker network create curso 
docker inspect curso 

# crear contenedor de postgres
docker run --name postgres-database-curso --network curso -v D:/docker/postgrescurso:/var/lib/postgresql/data -e POSTGRES_PASSWORD=pass  -d -p 5434:5432  postgres


# ejecutar el docker compose
docker compose -f docker-compose.yml -f docker-compose.override.yml up -d


# crear migraciones

dotnet ef --verbose migrations add InitialCreate -p src/CleanArchitecture/CleanArchitecture.Infraestructure -s src/CleanArchitecture/CleanArchitecture.Api

dotnet ef migrations add OutboxCreationMigrations -p src/CleanArchitecture/CleanArchitecture.Infraestructure -s src/CleanArchitecture/CleanArchitecture.Api


# Ejecutar pruebas
dotnet test CleanArchitecture.sln