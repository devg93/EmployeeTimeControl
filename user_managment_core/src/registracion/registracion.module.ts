import { Module } from '@nestjs/common';
import { RegistracionService } from './registracion.service';
import { RegistracionController } from './registracion.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { User } from './entities/registracion.entity';
import * as dotenv from 'dotenv';

dotenv.config();

@Module({
  imports: [
    TypeOrmModule.forRoot({
      type: 'mysql',
      host: process.env.DB_HOST || 'localhost',
      port: Number(process.env.DB_PORT) || 3306,
      username: process.env.DB_USER || 'root',
      password: process.env.DB_PASSWORD || 'password',
      database: process.env.DB_NAME || 'userdatabase',
      autoLoadEntities: true, 
      synchronize: true,
    }),
    TypeOrmModule.forFeature([User]),
  ],
  controllers: [RegistracionController],
  providers: [RegistracionService],
  exports: [TypeOrmModule, TypeOrmModule.forFeature([User])], 
})
export class RegistracionModule {}
