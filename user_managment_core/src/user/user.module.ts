import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AuthRepository } from './user.repository.services/authRepository.service';
import { JwtModule } from '@nestjs/jwt';
import { JwtStrategy } from './libs/JwtStrategy';

import { User } from 'src/user/entities/registracion.entity';
import { PassportModule } from '@nestjs/passport';

import { RegistracionController } from './user.controllers/registracion.controller';
import { AuthController } from './user.controllers/auth.controller';
import { RegistracionRepository } from './user.repository.services/regiRepository.service';
import { UserEventsSubscriber } from './services/cdc.service.mongoDb';
import { RedisService } from './services/redis.service';



@Module({
  imports: [
     
    TypeOrmModule.forFeature([User]), 
 
    PassportModule.register({ defaultStrategy: 'jwt' }),

    JwtModule.register({
      secret: process.env.JWT_SECRET || 'default_secret_key',
      signOptions: { expiresIn: '1h' },
    }),
  ], 
  controllers: [AuthController,RegistracionController],
  providers: [AuthRepository, JwtStrategy,RegistracionRepository,UserEventsSubscriber,RedisService],
  exports: [AuthRepository, JwtModule, PassportModule],
})
export class userModule {}
