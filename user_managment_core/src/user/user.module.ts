import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AuthRepository } from './user.repository/authRepository.service';
import { JwtModule } from '@nestjs/jwt';
import { JwtStrategy } from './libs/JwtStrategy';

import { User } from 'src/user/entities/registracion.entity';
import { PassportModule } from '@nestjs/passport';

import { RegistracionController } from './user.controllers/user.controller';
import { AuthController } from './user.controllers/auth.controller';
import { UserRepository } from './user.repository/userRepository.service';
import { UserEventsSubscriber } from './user.services/cdc.service.mongoDb';
import { RedisService } from './user.services/redis.service';
import { RedisModule } from 'src/rediscache/rediscache.module';
import { UserService } from './user.services/user.service ';



@Module({
  imports: [RedisModule,

    TypeOrmModule.forFeature([User]),

    PassportModule.register({ defaultStrategy: 'jwt' }),

    JwtModule.register({
      secret: process.env.JWT_SECRET || 'default_secret_key',
      signOptions: { expiresIn: '1h' },
    }),
  ],
  controllers: [AuthController, RegistracionController],
  providers: [

    {
      provide: 'Userrepositoryinterface',
      useClass: AuthRepository

    }, AuthRepository,

    {
      provide: 'Userrepositoryinterface',
      useClass: UserRepository
    }, UserRepository,
    
    {
      provide: 'UserInterface',
      useClass: UserService
    }, UserService,

    UserEventsSubscriber,

  JwtStrategy

  , RedisService

  ],
  exports: [AuthRepository, JwtModule, PassportModule],
})
export class userModule { }
