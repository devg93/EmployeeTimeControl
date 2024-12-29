import { Injectable } from '@nestjs/common';
import { CreateGetdatumDto } from './dto/create-getdatum.dto';
import { UpdateGetdatumDto } from './dto/update-getdatum.dto';

@Injectable()
export class GetdataService {
  create(createGetdatumDto: CreateGetdatumDto) {
    return 'This action adds a new getdatum';
  }

  findAll() {
    return `This action returns all getdata`;
  }

  findOne(id: number) {
    return `This action returns a #${id} getdatum`;
  }

  update(id: number, updateGetdatumDto: UpdateGetdatumDto) {
    return `This action updates a #${id} getdatum`;
  }

  remove(id: number) {
    return `This action removes a #${id} getdatum`;
  }
}
