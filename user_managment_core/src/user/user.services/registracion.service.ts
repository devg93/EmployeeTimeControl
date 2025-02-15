import { Injectable } from '@nestjs/common';
import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';


@Injectable()
export class RegistracionService {
  create(createRegistracionDto: CreateRegistracionDto) {
    return 'This action adds a new registracion';
  }

  findAll() {
    return `This action returns all registracion`;
  }

  findOne(id: number) {
    return `This action returns a #${id} registracion`;
  }

  update(id: number, updateRegistracionDto: UpdateRegistracionDto) {
    return `This action updates a #${id} registracion`;
  }

  remove(id: number) {
    return `This action removes a #${id} registracion`;
  }
}
