import { Module } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { RegistracionModule } from './registracion/registracion.module';

@Module({
  imports: [AuthModule, RegistracionModule],
  controllers: [],
  providers: [],
})
export class AppModule {}
