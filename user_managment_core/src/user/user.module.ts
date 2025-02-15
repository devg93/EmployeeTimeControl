import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AuthService } from './user.services/auth.service';
import { JwtModule } from '@nestjs/jwt';
import { JwtStrategy } from './libs/JwtStrategy';

import { User } from 'src/user/entities/registracion.entity';
import { PassportModule } from '@nestjs/passport';

import { RegistracionController } from './user.controllers/registracion.controller';
import { AuthController } from './user.controllers/auth.controller';
import { RegistracionService } from './user.services/registracion.service';

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
  providers: [AuthService, JwtStrategy,RegistracionService],
  exports: [AuthService, JwtModule, PassportModule],
})
export class userModule {}
