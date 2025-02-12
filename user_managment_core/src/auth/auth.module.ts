import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AuthService } from './auth.service';
import { AuthController } from './auth.controller';
import { JwtModule } from '@nestjs/jwt';
import { JwtStrategy } from './libs/JwtStrategy';
import { RegistracionModule } from 'src/registracion/registracion.module';
import { User } from 'src/registracion/entities/registracion.entity';
import { PassportModule } from '@nestjs/passport';

@Module({
  imports: [
    RegistracionModule, 
    TypeOrmModule.forFeature([User]), 
    
 
    PassportModule.register({ defaultStrategy: 'jwt' }),

    JwtModule.register({
      secret: process.env.JWT_SECRET || 'default_secret_key',
      signOptions: { expiresIn: '1h' },
    }),
  ], 
  controllers: [AuthController],
  providers: [AuthService, JwtStrategy],
  exports: [AuthService, JwtModule, PassportModule],
})
export class AuthModule {}
