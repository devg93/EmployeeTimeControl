import { Module } from '@nestjs/common';
import { RegistracionService } from './registracion.service';
import { RegistracionController } from './registracion.controller';

@Module({
  controllers: [RegistracionController],
  providers: [RegistracionService],
})
export class RegistracionModule {}
