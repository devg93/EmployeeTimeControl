import { PartialType } from '@nestjs/mapped-types';
import { CreateRegistracionDto } from './create-registracion.dto';

export class UpdateRegistracionDto extends PartialType(CreateRegistracionDto) {}
