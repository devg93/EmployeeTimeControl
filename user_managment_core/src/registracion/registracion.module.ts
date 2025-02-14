import { Module } from '@nestjs/common';
import { RegistracionService } from './registracion.service';
import { RegistracionController } from './registracion.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from './entities/registracion.entity';
import * as dotenv from 'dotenv';

dotenv.config();

const typeOrmModule = TypeOrmModule.forRoot({
  type: 'mysql',
  replication: {
    master: {
      host: process.env.MASTER_DB_HOST,  
      port: +process.env.DB_PORT || 3306,
      username: process.env.MASTER_DB_USERNAME || 'root',
      password: process.env.MASTER_DB_PASSWORD || 'password',
      database: process.env.MASTER_DB_NAME || 'userdatabase',
    },
    slaves: [
      {
        host: process.env.REPLICA_DB_HOST,  
        port: +process.env.DB_PORT || 3306,
        username: process.env.REPLICA_DB_USERNAME || 'replica_user',
        password: process.env.REPLICA_DB_PASSWORD || 'strongpassword',
        database: process.env.REPLICA_DB_NAME || 'userdatabase',
      },
    ],
  },
  entities: [__dirname + '/../**/*.entity{.ts,.js}'],
  synchronize: false, 
  logging: ['query', 'error'], 
});

@Module({
  imports: [typeOrmModule],
  controllers: [RegistracionController],
  providers: [RegistracionService],
  exports: [TypeOrmModule, TypeOrmModule.forFeature([User])], 
})
export class RegistracionModule {}
